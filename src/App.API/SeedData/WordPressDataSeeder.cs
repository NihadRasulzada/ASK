using System.Reflection;
using System.Text.Json;
using App.BL.Services.External;
using App.Core.Entities;
using App.Core.Entities.Common.Storage;
using App.Core.Enums;
using App.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace App.API.SeedData;

public static class WordPressDataSeeder
{
    private const string SeedPath = "SeedData/wordpress-seed.json";
    private const string SeedMediaPath = "SeedData/wordpress-media";
    private const string SeedMediaPrefix = "seed-media/";
    private const string MinioSeedPrefix = "wordpress-seed/";
    private const string SeedMediaReadyMarker = $"{MinioSeedPrefix}.seed-media-ready";
    private const string FallbackImage = "https://ask.org.az/wp-content/uploads/2025/08/ASK-logo-600x400.jpg";

    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration, ILogger logger, CancellationToken cancellationToken = default)
    {
        if (!configuration.GetValue("WordPressSeed:Enabled", true))
            return;

        var fullPath = Path.Combine(AppContext.BaseDirectory, SeedPath);
        if (!File.Exists(fullPath))
        {
            logger.LogWarning("WordPress seed file was not found at {SeedPath}", fullPath);
            return;
        }

        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var objectStorageService = scope.ServiceProvider.GetRequiredService<IObjectStorageService>();

        await using var stream = File.OpenRead(fullPath);
        var seed = await JsonSerializer.DeserializeAsync<WordPressSeed>(stream, JsonOptions, cancellationToken);
        if (seed is null)
        {
            logger.LogWarning("WordPress seed file is empty or invalid: {SeedPath}", fullPath);
            return;
        }

        await UploadSeedMediaDirectoryAsync(objectStorageService, logger, cancellationToken);

        var added = 0;
        Task<StoredFile> ToStoredMediaAsync(MediaSeed? mediaSeed)
            => ToStoredMediaAsync(mediaSeed, objectStorageService, logger, cancellationToken);

        if (!await db.News.AnyAsync(cancellationToken))
        {
            foreach (var item in seed.News)
            {
                var titleAz = Required(item.TitleAz, "Xəbər");
                var news = new News(
                    await ToStoredMediaAsync(item.TitleImage),
                    titleAz,
                    Required(item.TitleEn, titleAz),
                    Required(item.TitleRu, titleAz),
                    Required(item.TextAz, titleAz),
                    Required(item.TextEn, item.TextAz, titleAz),
                    Required(item.TextRu, item.TextAz, titleAz));

                Set(news, nameof(News.CreateDate), ParseDate(item.Created));
                db.News.Add(news);

                foreach (var image in item.Images)
                    db.Set<NewsImage>().Add(new NewsImage(await ToStoredMediaAsync(image), news.Id));

                added++;
            }
        }

        if (!await db.Announcements.AnyAsync(cancellationToken))
        {
            foreach (var item in seed.Announcements)
            {
                var titleAz = Required(item.TitleAz, "Elan");
                var announcement = new Announcement(
                    titleAz,
                    Required(item.TitleEn, titleAz),
                    Required(item.TitleRu, titleAz),
                    await ToStoredMediaAsync(item.TitleImage),
                    Required(item.TextAz, titleAz),
                    Required(item.TextEn, item.TextAz, titleAz),
                    Required(item.TextRu, item.TextAz, titleAz));

                Set(announcement, nameof(Announcement.Created), ParseDate(item.Created));
                db.Announcements.Add(announcement);
                added++;
            }
        }

        if (!await db.Exhibitions.AnyAsync(cancellationToken))
            added += await AddEventsAsync(db, seed.Exhibitions, async item => new Exhibition(
                Required(item.TitleAz, "Sərgi"),
                Required(item.TitleEn, item.TitleAz, "Exhibition"),
                Required(item.TitleRu, item.TitleAz, "Выставка"),
                await ToStoredMediaAsync(item.TitleImage),
                Required(item.TextAz, item.TitleAz),
                Required(item.TextEn, item.TextAz, item.TitleAz),
                Required(item.TextRu, item.TextAz, item.TitleAz),
                ParseDate(item.StartDate),
                ParseDate(item.EndDate)));

        if (!await db.Training.AnyAsync(cancellationToken))
            added += await AddEventsAsync(db, seed.Trainings, async item => new Training(
                Required(item.TitleAz, "Təlim"),
                Required(item.TitleEn, item.TitleAz, "Training"),
                Required(item.TitleRu, item.TitleAz, "Тренинг"),
                await ToStoredMediaAsync(item.TitleImage),
                Required(item.TextAz, item.TitleAz),
                Required(item.TextEn, item.TextAz, item.TitleAz),
                Required(item.TextRu, item.TextAz, item.TitleAz),
                ParseDate(item.StartDate),
                ParseDate(item.EndDate)));

        if (!await db.BusinessForums.AnyAsync(cancellationToken))
        {
            foreach (var item in seed.BusinessForums)
            {
                var eventDate = ParseDate(item.Created);
                db.BusinessForums.Add(new BusinessForum(
                    Required(item.TitleAz, "Biznes forum"),
                    Required(item.TitleEn, item.TitleAz, "Business forum"),
                    Required(item.TitleRu, item.TitleAz, "Бизнес форум"),
                    await ToStoredMediaAsync(item.TitleImage),
                    Required(item.TextAz, item.TitleAz),
                    Required(item.TextEn, item.TextAz, item.TitleAz),
                    Required(item.TextRu, item.TextAz, item.TitleAz),
                    eventDate,
                    eventDate,
                    await ToStoredMediaAsync(item.DetailImage)));
                added++;
            }
        }

        if (!await db.Directors.AnyAsync(cancellationToken))
            added += await AddRangeAsync(db, seed.Directors, async x => new Director(
                await ToStoredMediaAsync(x.Image),
                Required(x.FullNameAz, "Direktor"),
                Required(x.FullNameEn, x.FullNameAz, "Director"),
                Required(x.FullNameRu, x.FullNameAz, "Директор"),
                Required(x.DutyAz, "-"),
                Required(x.DutyEn, x.DutyAz, "-"),
                Required(x.DutyRu, x.DutyAz, "-"),
                x.DepartmentAz ?? "",
                x.DepartmentEn ?? "",
                x.DepartmentRu ?? "",
                x.PhoneNumber ?? "",
                x.Email ?? ""));

        if (!await db.Management.AnyAsync(cancellationToken))
            added += AddRange(db, seed.Management.Select(x => new Management(
                Required(x.FullNameAz, "Üzv"),
                Required(x.FullNameEn, x.FullNameAz, "Member"),
                Required(x.FullNameRu, x.FullNameAz, "Член"),
                Required(x.CompanyAz, "-"),
                Required(x.CompanyEn, x.CompanyAz, "-"),
                Required(x.CompanyRu, x.CompanyAz, "-"))));

        if (!await db.Committees.AnyAsync(cancellationToken))
            added += AddRange(db, seed.Committees.Select(x => new Committee(
                Required(x.NameAz, "Komissiya"),
                Required(x.NameEn, x.NameAz, "Committee"),
                Required(x.NameRu, x.NameAz, "Комиссия"),
                Required(x.ChairmanAz, "-"),
                Required(x.ChairmanEn, x.ChairmanAz, "-"),
                Required(x.ChairmanRu, x.ChairmanAz, "-"),
                Required(x.VicePresidentAz, "-"),
                Required(x.VicePresidentEn, x.VicePresidentAz, "-"),
                Required(x.VicePresidentRu, x.VicePresidentAz, "-"))));

        if (!await db.DistrictRepresentatives.AnyAsync(cancellationToken))
            added += AddRange(db, seed.DistrictRepresentatives.Select(x => new DistrictRepresentatives(
                Required(x.DistrictAz, "-"),
                Required(x.DistrictEn, x.DistrictAz, "-"),
                Required(x.DistrictRu, x.DistrictAz, "-"),
                Required(x.FullNameAz, "-"),
                Required(x.FullNameEn, x.FullNameAz, "-"),
                Required(x.FullNameRu, x.FullNameAz, "-"),
                Required(x.CompanyAz, "-"),
                Required(x.CompanyEn, x.CompanyAz, "-"),
                Required(x.CompanyRu, x.CompanyAz, "-"))));

        if (!await db.ForeignRepresentatives.AnyAsync(cancellationToken))
            added += AddRange(db, seed.ForeignRepresentatives.Select(x => new ForeignRepresentatives(
                Required(x.CountryAz, "-"),
                Required(x.CountryEn, x.CountryAz, "-"),
                Required(x.CountryRu, x.CountryAz, "-"),
                Required(x.FullNameAz, "-"),
                Required(x.FullNameEn, x.FullNameAz, "-"),
                Required(x.FullNameRu, x.FullNameAz, "-"),
                Required(x.CompanyAz, "-"),
                Required(x.CompanyEn, x.CompanyAz, "-"),
                Required(x.CompanyRu, x.CompanyAz, "-"),
                Required(x.DutyAz, "-"),
                Required(x.DutyEn, x.DutyAz, "-"),
                Required(x.DutyRu, x.DutyAz, "-"))));

        if (!await db.Publications.AnyAsync(cancellationToken))
            added += await AddRangeAsync(db, seed.Publications, async x => new Publication(
                await ToStoredMediaAsync(x.TitleImage),
                Required(x.TitleAz, "Nəşr"),
                Required(x.TitleEn, x.TitleAz, "Publication"),
                Required(x.TitleRu, x.TitleAz, "Публикация"),
                await ToStoredMediaAsync(x.Pdf)));

        if (!await db.Partners.AnyAsync(cancellationToken))
            added += await AddRangeAsync(db, seed.Partners, async x => new Partner(await ToStoredMediaAsync(x.Image), Required(x.Site, "#")));

        if (!await db.InternationalSolidarity.AnyAsync(cancellationToken))
            added += await AddRangeAsync(db, seed.InternationalSolidarity, async x => new InternationalSolidarity(Required(x.Link, "#"), await ToStoredMediaAsync(x.Icon)));

        if (!await db.Galleries.AnyAsync(cancellationToken))
            added += await AddRangeAsync(db, seed.Galleries, async x => new Gallery(await ToStoredMediaAsync(x.Image)));

        if (!await db.FAQs.AnyAsync(cancellationToken))
            added += AddRange(db, seed.Faqs.Select(x => new FAQ(
                Required(x.QuestionAz, "Sual"),
                Required(x.QuestionEn, x.QuestionAz, "Question"),
                Required(x.QuestionRu, x.QuestionAz, "Вопрос"),
                Required(x.AnswerAz, "-"),
                Required(x.AnswerEn, x.AnswerAz, "-"),
                Required(x.AnswerRu, x.AnswerAz, "-"))));

        if (!await db.Presidents.AnyAsync(cancellationToken) && seed.President is not null)
        {
            db.Presidents.Add(new President(await ToStoredMediaAsync(seed.President.Image), Required(seed.President.Text, "President")));
            added++;
        }

        if (!await db.Services.AnyAsync(cancellationToken))
            added += await AddRangeAsync(db, seed.Services, async x => new Service(
                await ToStoredMediaAsync(x.Image),
                Required(x.NameAz, "Xidmət"),
                Required(x.NameEn, x.NameAz, "Service"),
                Required(x.NameRu, x.NameAz, "Услуга")));

        if (!await db.UsefulLinks.AnyAsync(cancellationToken))
            added += AddRange(db, seed.UsefulLinks.Select(x => new UsefulLink(
                Required(x.TitleAz, "Link"),
                Required(x.TitleEn, x.TitleAz, "Link"),
                Required(x.TitleRu, x.TitleAz, "Link"),
                Required(x.Link, "#"))));

        foreach (var settingSeed in seed.Settings)
        {
            var setting = await db.Settings.FirstOrDefaultAsync(x => x.Key == settingSeed.Key, cancellationToken);
            if (setting?.ValueType == SettingValueType.Text && string.IsNullOrWhiteSpace(setting.StringValue))
                setting.UpdateStringValue(settingSeed.Value);
        }

        if (db.ChangeTracker.HasChanges())
        {
            await db.SaveChangesAsync(cancellationToken);
            logger.LogInformation("WordPress seed completed. Added approximately {Count} root records.", added);
        }
    }

    private static async Task<int> AddEventsAsync<T>(AppDbContext db, IEnumerable<WordPressEventSeed> items, Func<WordPressEventSeed, Task<T>> factory)
        where T : Event
    {
        var count = 0;
        foreach (var item in items)
        {
            var entity = await factory(item);
            Set(entity, nameof(Event.Created), ParseDate(item.Created));
            db.Add(entity);
            count++;
        }

        return count;
    }

    private static async Task<int> AddRangeAsync<TSeed, TEntity>(AppDbContext db, IEnumerable<TSeed> items, Func<TSeed, Task<TEntity>> factory)
        where TEntity : class
    {
        var list = new List<TEntity>();
        foreach (var item in items)
            list.Add(await factory(item));

        db.Set<TEntity>().AddRange(list);
        return list.Count;
    }

    private static int AddRange<TEntity>(AppDbContext db, IEnumerable<TEntity> entities)
        where TEntity : class
    {
        var list = entities.ToList();
        db.Set<TEntity>().AddRange(list);
        return list.Count;
    }

    private static async Task UploadSeedMediaDirectoryAsync(IObjectStorageService objectStorageService, ILogger logger, CancellationToken cancellationToken)
    {
        var mediaRoot = Path.Combine(AppContext.BaseDirectory, SeedMediaPath);
        if (!Directory.Exists(mediaRoot))
        {
            logger.LogWarning("WordPress seed media directory was not found at {SeedMediaPath}", mediaRoot);
            return;
        }

        try
        {
            var readyMarker = await objectStorageService.GetAsync(SeedMediaReadyMarker, cancellationToken);
            if (readyMarker is not null)
            {
                await readyMarker.Stream.DisposeAsync();
                return;
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Could not check WordPress seed media marker. Media upload will continue file by file.");
        }

        var failures = 0;
        foreach (var filePath in Directory.EnumerateFiles(mediaRoot, "*", SearchOption.AllDirectories))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var relativePath = Path.GetRelativePath(mediaRoot, filePath).Replace(Path.DirectorySeparatorChar, '/');
            var objectName = $"{MinioSeedPrefix}{relativePath}";

            try
            {
                await using var file = File.OpenRead(filePath);
                await objectStorageService.UploadAsync(file, objectName, GuessContentType(filePath), file.Length, cancellationToken);
            }
            catch (Exception ex)
            {
                failures++;
                logger.LogWarning(ex, "Failed to upload WordPress seed media {MediaPath} to object storage.", filePath);
            }
        }

        if (failures == 0)
        {
            await using var marker = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString("O")));
            await objectStorageService.UploadAsync(marker, SeedMediaReadyMarker, "text/plain", marker.Length, cancellationToken);
            logger.LogInformation("WordPress seed media uploaded to object storage.");
        }
        else
        {
            logger.LogWarning("WordPress seed media upload finished with {Count} failed files. It will retry on the next startup.", failures);
        }
    }

    private static async Task<StoredFile> ToStoredMediaAsync(MediaSeed? mediaSeed, IObjectStorageService objectStorageService, ILogger logger, CancellationToken cancellationToken)
    {
        var url = Required(mediaSeed?.Url, FallbackImage);

        if (!url.StartsWith(SeedMediaPrefix, StringComparison.OrdinalIgnoreCase))
            return new StoredFile(url);

        var relativePath = url[SeedMediaPrefix.Length..].TrimStart('/');
        var objectName = $"{MinioSeedPrefix}{relativePath}";
        var mediaPath = Path.Combine(AppContext.BaseDirectory, SeedMediaPath, relativePath.Replace('/', Path.DirectorySeparatorChar));

        if (!File.Exists(mediaPath))
        {
            logger.LogWarning("Seed media file was not found: {MediaPath}", mediaPath);
            return new StoredFile(objectName);
        }

        try
        {
            await using var file = File.OpenRead(mediaPath);
            return await objectStorageService.UploadAsync(file, objectName, GuessContentType(mediaPath), file.Length, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to upload seed media {MediaPath}. Keeping object key in database.", mediaPath);
            return new StoredFile(objectName);
        }
    }

    private static string GuessContentType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream"
        };
    }

    private static DateTime ParseDate(string? value)
        => DateTime.TryParse(value, out var date) ? date : DateTime.UtcNow;

    private static string Required(params string?[] values)
        => values.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x))?.Trim() ?? "-";

    private static void Set<T>(T entity, string propertyName, object value)
    {
        var property = typeof(T).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            ?? entity!.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        property?.SetValue(entity, value);
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private sealed record WordPressSeed
    {
        public List<WordPressNewsSeed> News { get; init; } = [];
        public List<WordPressNewsSeed> Announcements { get; init; } = [];
        public List<WordPressEventSeed> Exhibitions { get; init; } = [];
        public List<WordPressEventSeed> Trainings { get; init; } = [];
        public List<WordPressBusinessForumSeed> BusinessForums { get; init; } = [];
        public List<DirectorSeed> Directors { get; init; } = [];
        public List<ManagementSeed> Management { get; init; } = [];
        public List<CommitteeSeed> Committees { get; init; } = [];
        public List<DistrictRepresentativeSeed> DistrictRepresentatives { get; init; } = [];
        public List<ForeignRepresentativeSeed> ForeignRepresentatives { get; init; } = [];
        public List<PublicationSeed> Publications { get; init; } = [];
        public List<PartnerSeed> Partners { get; init; } = [];
        public List<InternationalSolidaritySeed> InternationalSolidarity { get; init; } = [];
        public List<GallerySeed> Galleries { get; init; } = [];
        public List<FaqSeed> Faqs { get; init; } = [];
        public PresidentSeed? President { get; init; }
        public List<SettingSeed> Settings { get; init; } = [];
        public List<ServiceSeed> Services { get; init; } = [];
        public List<UsefulLinkSeed> UsefulLinks { get; init; } = [];
    }

    private sealed record MediaSeed(string Url, string? PublicId = null);

    private sealed record WordPressNewsSeed(
        string Created,
        string TitleAz,
        string TitleEn,
        string TitleRu,
        string TextAz,
        string TextEn,
        string TextRu,
        MediaSeed TitleImage,
        List<MediaSeed> Images);

    private sealed record WordPressEventSeed(
        string Created,
        string StartDate,
        string EndDate,
        string TitleAz,
        string TitleEn,
        string TitleRu,
        string TextAz,
        string TextEn,
        string TextRu,
        MediaSeed TitleImage);

    private sealed record WordPressBusinessForumSeed(
        string Created,
        string TitleAz,
        string TitleEn,
        string TitleRu,
        string TextAz,
        string TextEn,
        string TextRu,
        MediaSeed TitleImage,
        MediaSeed DetailImage);

    private sealed record DirectorSeed(MediaSeed Image, string FullNameAz, string FullNameEn, string FullNameRu, string DutyAz, string DutyEn, string DutyRu, string? DepartmentAz, string? DepartmentEn, string? DepartmentRu, string? PhoneNumber, string? Email);
    private sealed record ManagementSeed(string FullNameAz, string FullNameEn, string FullNameRu, string CompanyAz, string CompanyEn, string CompanyRu);
    private sealed record CommitteeSeed(string NameAz, string NameEn, string NameRu, string ChairmanAz, string ChairmanEn, string ChairmanRu, string VicePresidentAz, string VicePresidentEn, string VicePresidentRu);
    private sealed record DistrictRepresentativeSeed(string DistrictAz, string DistrictEn, string DistrictRu, string FullNameAz, string FullNameEn, string FullNameRu, string CompanyAz, string CompanyEn, string CompanyRu);
    private sealed record ForeignRepresentativeSeed(string CountryAz, string CountryEn, string CountryRu, string FullNameAz, string FullNameEn, string FullNameRu, string CompanyAz, string CompanyEn, string CompanyRu, string DutyAz, string DutyEn, string DutyRu);
    private sealed record PublicationSeed(string TitleAz, string TitleEn, string TitleRu, MediaSeed TitleImage, MediaSeed Pdf);
    private sealed record PartnerSeed(MediaSeed Image, string Site);
    private sealed record InternationalSolidaritySeed(string Link, MediaSeed Icon);
    private sealed record GallerySeed(MediaSeed Image);
    private sealed record FaqSeed(string QuestionAz, string QuestionEn, string QuestionRu, string AnswerAz, string AnswerEn, string AnswerRu);
    private sealed record PresidentSeed(string Text, MediaSeed Image);
    private sealed record SettingSeed(string Key, string Value);
    private sealed record ServiceSeed(string NameAz, string NameEn, string NameRu, MediaSeed Image);
    private sealed record UsefulLinkSeed(string TitleAz, string TitleEn, string TitleRu, string Link);
}
