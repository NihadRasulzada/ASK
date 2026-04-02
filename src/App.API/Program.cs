using App.API.BackgroundJobs;
using App.API.Filters;
using App.API.Middleware;
using App.API.Services;
using App.BL.Services.Business.CurrencyRate;
using App.BL.Services.Business.Director;
using App.BL.Services.Business.News;
using App.BL.Services.Business.Service;
using App.BL.Services.External;
using App.BL.Settings;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.CurrencyRate;
using App.Core.Interfaces.Repository.Director;
using App.Core.Interfaces.Repository.Gallery;
using App.Core.Interfaces.Repository.News;
using App.Core.Interfaces.Repository.NewsImage;
using App.Core.Interfaces.Repository.Partner;
using App.Core.Interfaces.Repository.President;
using App.Core.Interfaces.Repository.Service;
using App.Core.Interfaces.Repository.Video;
using App.Core.Interfaces.Repository.Training;
using App.Core.Interfaces.Repository.Exhibition;
using App.DAL.Context;
using App.DAL.Repositories.Common;
using App.DAL.Repositories.CurrencyRate;
using App.DAL.Repositories.Director;
using App.DAL.Repositories.Gallery;
using App.DAL.Repositories.News;
using App.DAL.Repositories.NewsImage;
using App.DAL.Repositories.Partner;
using App.DAL.Repositories.President;
using App.DAL.Repositories.Service;
using App.DAL.Repositories.Video;
using App.DAL.Repositories.Training;
using App.DAL.Repositories.Exhibition;
using App.BL.Services.Business.Video;
using App.BL.Services.Business.Gallery;
using App.BL.Services.Business.Announcement;
using App.BL.Services.Business.Partner;
using App.BL.Services.Business.President;
using App.BL.Services.Business.Training;
using App.BL.Services.Business.Exhibition;
using App.BL.Mapper.Video;
using App.BL.Mapper.Gallery;
using App.BL.Mapper.Announcement;
using App.BL.Mapper.Partner;
using App.BL.Mapper.President;
using App.BL.Mapper.Training;
using App.BL.Mapper.Exhibition;
using App.BL.Mapper.Director;
using App.BL.Mapper.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using App.BL.Validators.Video;
using App.Core.Interfaces.Repository.Management;
using App.Core.Interfaces.Repository.InternationalSolidarity;
using App.Core.Interfaces.Repository.ForeignRepresentatives;
using App.Core.Interfaces.Repository.DistrictRepresentatives;
using App.Core.Interfaces.Repository.Committee;
using App.Core.Interfaces.Repository.Presidium;
using App.Core.Interfaces.Repository.OurValues;
using App.Core.Interfaces.Repository.Settings;
using App.DAL.Repositories.Settings;
using App.BL.Mapper.Setting;
using App.BL.Services.Business.Setting;
using App.DAL.Repositories.Management;
using App.DAL.Repositories.InternationalSolidarity;
using App.DAL.Repositories.ForeignRepresentatives;
using App.DAL.Repositories.DistrictRepresentatives;
using App.DAL.Repositories.Committee;
using App.DAL.Repositories.Presidium;
using App.DAL.Repositories.OurValues;
using App.BL.Mapper.Management;
using App.BL.Mapper.InternationalSolidarity;
using App.BL.Mapper.ForeignRepresentatives;
using App.BL.Mapper.DistrictRepresentatives;
using App.BL.Mapper.Committee;
using App.BL.Mapper.Presidium;
using App.BL.Mapper.OurValues;
using App.BL.Services.Business.Management;
using App.BL.Services.Business.InternationalSolidarity;
using App.BL.Services.Business.ForeignRepresentatives;
using App.BL.Services.Business.DistrictRepresentatives;
using App.BL.Services.Business.Committee;
using App.BL.Services.Business.Presidium;
using App.BL.Services.Business.OurValues;

var builder = WebApplication.CreateBuilder(args);

// ── CORS ──────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// ── Swagger / OpenAPI ─────────────────────────────────────────────────────────
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Application API",
        Version = "v1",
        Description = "Application Backend API"
    });

    // Controller XML doc comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);

    // JWT Bearer auth
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ── Database ──────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ── FluentValidation ──────────────────────────────────────────────────────────
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateVideoDtoValidator>();

// ── Exception Handling ────────────────────────────────────────────────────────
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// ── Filters ───────────────────────────────────────────────────────────────────
builder.Services.AddScoped<ValidationFilter>();

// ── Memory Cache ──────────────────────────────────────────────────────────────
builder.Services.AddMemoryCache();

// ── Cloudinary ────────────────────────────────────────────────────────────────
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));

// ── ExchangeRate ──────────────────────────────────────────────────────────────
builder.Services.Configure<ExchangeRateSettings>(
    builder.Configuration.GetSection("ExchangeRateSettings"));

// ── HTTP Clients ──────────────────────────────────────────────────────────────
builder.Services.AddHttpClient("ExchangeRate", (sp, client) =>
{
    var s = sp.GetRequiredService<IOptions<ExchangeRateSettings>>().Value;
    client.BaseAddress = new Uri($"{s.BaseUrl}/");
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient("CloudinaryProxy", client =>
{
    client.BaseAddress = new Uri("https://res.cloudinary.com/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// ── HttpContextAccessor / CurrentUser ─────────────────────────────────────────
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// ── Repositories ──────────────────────────────────────────────────────────────
builder.Services.AddScoped<IVideoReadRepository, VideoReadRepository>();
builder.Services.AddScoped<IVideoWriteRepository, VideoWriteRepository>();

builder.Services.AddScoped<IPartnerReadRepository, PartnerReadRepository>();
builder.Services.AddScoped<IPartnerWriteRepository, PartnerWriteRepository>();

builder.Services.AddScoped<IPresidentReadRepository, PresidentReadRepository>();
builder.Services.AddScoped<IPresidentWriteRepository, PresidentWriteRepository>();

builder.Services.AddScoped<ITrainingReadRepository, TrainingReadRepository>();
builder.Services.AddScoped<ITrainingWriteRepository, TrainingWriteRepository>();

builder.Services.AddScoped<IExhibitionReadRepository, ExhibitionReadRepository>();
builder.Services.AddScoped<IExhibitionWriteRepository, ExhibitionWriteRepository>();

builder.Services.AddScoped<INewsReadRepository, NewsReadRepository>();
builder.Services.AddScoped<INewsWriteRepository, NewsWriteRepository>();

builder.Services.AddScoped<INewsImageReadRepository, NewsImageReadRepository>();
builder.Services.AddScoped<INewsImageWriteRepository, NewsImageWriteRepository>();

builder.Services.AddScoped<IDirectorReadRepository, DirectorReadRepository>();
builder.Services.AddScoped<IDirectorWriteRepository, DirectorWriteRepository>();

builder.Services.AddScoped<IServiceReadRepository, ServiceReadRepository>();
builder.Services.AddScoped<IServiceWriteRepository, ServiceWriteRepository>();

builder.Services.AddScoped<IGalleryReadRepository, GalleryReadRepository>();
builder.Services.AddScoped<IGalleryWriteRepository, GalleryWriteRepository>();

builder.Services.AddScoped<App.Core.Interfaces.Repository.Announcement.IAnnouncementReadRepository, App.DAL.Repositories.Announcement.AnnouncementReadRepository>();
builder.Services.AddScoped<App.Core.Interfaces.Repository.Announcement.IAnnouncementWriteRepository, App.DAL.Repositories.Announcement.AnnouncementWriteRepository>();

builder.Services.AddScoped<ICurrencyRateReadRepository, CurrencyRateReadRepository>();

builder.Services.AddScoped<IManagementReadRepository, ManagementReadRepository>();
builder.Services.AddScoped<IManagementWriteRepository, ManagementWriteRepository>();

builder.Services.AddScoped<IInternationalSolidarityReadRepository, InternationalSolidarityReadRepository>();
builder.Services.AddScoped<IInternationalSolidarityWriteRepository, InternationalSolidarityWriteRepository>();

builder.Services.AddScoped<IForeignRepresentativesReadRepository, ForeignRepresentativesReadRepository>();
builder.Services.AddScoped<IForeignRepresentativesWriteRepository, ForeignRepresentativesWriteRepository>();

builder.Services.AddScoped<IDistrictRepresentativesReadRepository, DistrictRepresentativesReadRepository>();
builder.Services.AddScoped<IDistrictRepresentativesWriteRepository, DistrictRepresentativesWriteRepository>();

builder.Services.AddScoped<ICommitteeReadRepository, CommitteeReadRepository>();
builder.Services.AddScoped<ICommitteeWriteRepository, CommitteeWriteRepository>();

builder.Services.AddScoped<IPresidiumReadRepository, PresidiumReadRepository>();
builder.Services.AddScoped<IPresidiumWriteRepository, PresidiumWriteRepository>();

builder.Services.AddScoped<IOurValuesReadRepository, OurValuesReadRepository>();
builder.Services.AddScoped<IOurValuesWriteRepository, OurValuesWriteRepository>();

builder.Services.AddScoped<ISettingReadRepository, SettingReadRepository>();
builder.Services.AddScoped<ISettingWriteRepository, SettingWriteRepository>();

builder.Services.AddScoped<ILanguageService, LanguageService>();

// ── Mappers ───────────────────────────────────────────────────────────────────
builder.Services.AddScoped<App.BL.Mapper.News.INewsMapper, App.BL.Mapper.News.NewsMapper>();
builder.Services.AddScoped<App.BL.Mapper.NewsImage.INewsImageMapper, App.BL.Mapper.NewsImage.NewsImageMapper>();
builder.Services.AddScoped<IDirectorMapper, App.BL.Mapper.Director.DirectorMapper>();
builder.Services.AddScoped<IServiceMapper, App.BL.Mapper.Service.ServiceMapper>();
builder.Services.AddScoped<IVideoMapper, VideoMapper>();
builder.Services.AddScoped<IGalleryMapper, GalleryMapper>();
builder.Services.AddScoped<IAnnouncementMapper, AnnouncementMapper>();
builder.Services.AddScoped<IPartnerMapper, PartnerMapper>();
builder.Services.AddScoped<IPresidentMapper, PresidentMapper>();
builder.Services.AddScoped<ITrainingMapper, TrainingMapper>();
builder.Services.AddScoped<IExhibitionMapper, ExhibitionMapper>();
builder.Services.AddScoped<IManagementMapper, ManagementMapper>();
builder.Services.AddScoped<IInternationalSolidarityMapper, InternationalSolidarityMapper>();
builder.Services.AddScoped<IForeignRepresentativesMapper, ForeignRepresentativesMapper>();
builder.Services.AddScoped<IDistrictRepresentativesMapper, DistrictRepresentativesMapper>();
builder.Services.AddScoped<ICommitteeMapper, CommitteeMapper>();
builder.Services.AddScoped<IPresidiumMapper, PresidiumMapper>();
builder.Services.AddScoped<IOurValuesMapper, OurValuesMapper>();
builder.Services.AddScoped<ISettingMapper, SettingMapper>();

// ── Services ──────────────────────────────────────────────────────────────────
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IPresidentService, PresidentService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<App.BL.NewsImages.Business.NewsIamge.INewsImageService, App.BL.Services.Business.NewsIamge.NewsImageService>();
builder.Services.AddSingleton<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IManagementService, ManagementService>();
builder.Services.AddScoped<IInternationalSolidarityService, InternationalSolidarityService>();
builder.Services.AddScoped<IForeignRepresentativesService, ForeignRepresentativesService>();
builder.Services.AddScoped<IDistrictRepresentativesService, DistrictRepresentativesService>();
builder.Services.AddScoped<ICommitteeService, CommitteeService>();
builder.Services.AddScoped<IPresidiumService, PresidiumService>();
builder.Services.AddScoped<IOurValuesService, OurValuesService>();
builder.Services.AddScoped<ISettingService, SettingService>();

// ── Background Jobs ───────────────────────────────────────────────────────────
//builder.Services.AddHostedService<CurrencyBackgroundJob>();

// ─────────────────────────────────────────────────────────────────────────────

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LanguageMiddleware>();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
