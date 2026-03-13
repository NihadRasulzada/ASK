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
using App.BL.Validators;
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
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

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

// ── HttpContextAccessor / CurrentUser ─────────────────────────────────────────
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// ── Repositories ──────────────────────────────────────────────────────────────
builder.Services.AddScoped<IVideoReadRepository, VideoReadRepository>();
builder.Services.AddScoped<IVideoWriteRepository, VideoWriteRepository>();

builder.Services.AddScoped<IPartnerReadRepository, PartnerReadRepository>();
builder.Services.AddScoped<IPartnerWriteRepository, PartnerWriteRepository>();

builder.Services.AddScoped<ICurrencyRateReadRepository, CurrencyRateReadRepository>();

builder.Services.AddScoped<IPresidentReadRepository, PresidentReadRepository>();

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

builder.Services.AddScoped<ILanguageService, LanguageService>();

// ── Mappers ───────────────────────────────────────────────────────────────────
builder.Services.AddScoped<App.BL.Mapper.News.INewsMapper, App.BL.Mapper.News.NewsMapper>();
builder.Services.AddScoped<App.BL.Mapper.NewsImage.INewsImageMapper, App.BL.Mapper.NewsImage.NewsImageMapper>();
builder.Services.AddScoped<App.BL.Mapper.Director.IDirectorMapper, App.BL.Mapper.Director.DirectorMapper>();
builder.Services.AddScoped<App.BL.Mapper.Service.IServiceMapper, App.BL.Mapper.Service.ServiceMapper>();
builder.Services.AddScoped<App.BL.Mapper.Video.IVideoMapper, App.BL.Mapper.Video.VideoMapper>();
builder.Services.AddScoped<App.BL.Mapper.Gallery.IGalleryMapper, App.BL.Mapper.Gallery.GalleryMapper>();
builder.Services.AddScoped<App.BL.Mapper.Announcement.IAnnouncementMapper, App.BL.Mapper.Announcement.AnnouncementMapper>();
builder.Services.AddScoped<App.BL.Mapper.Partner.IPartnerMapper, App.BL.Mapper.Partner.PartnerMapper>();

// ── Services ──────────────────────────────────────────────────────────────────
builder.Services.AddScoped<App.BL.Services.Business.Video.IVideoService, App.BL.Services.Business.Video.VideoService>();
builder.Services.AddScoped<App.BL.Services.Business.Gallery.IGalleryService, App.BL.Services.Business.Gallery.GalleryService>();
builder.Services.AddScoped<App.BL.Services.Business.Announcement.IAnnouncementService, App.BL.Services.Business.Announcement.AnnouncementService>();
builder.Services.AddScoped<App.BL.Services.Business.Partner.IPartnerService, App.BL.Services.Business.Partner.PartnerService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
//builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<App.BL.NewsImages.Business.NewsIamge.INewsImageService, App.BL.Services.Business.NewsIamge.NewsImageService>();
builder.Services.AddSingleton<ICurrencyService, CurrencyService>();
//builder.Services.AddScoped<IPresidentService, PresidentService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

// ── Background Jobs ───────────────────────────────────────────────────────────
//builder.Services.AddHostedService<CurrencyBackgroundJob>();

// ─────────────────────────────────────────────────────────────────────────────

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LanguageMiddleware>();

app.MapControllers();

app.Run();
