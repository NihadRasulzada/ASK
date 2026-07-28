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
    private const string SeedDataReadyMarker = $"{MinioSeedPrefix}.seed-data-ready-20260728";
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

        if (await MarkerExistsAsync(objectStorageService, SeedDataReadyMarker, logger, cancellationToken))
        {
            logger.LogInformation("WordPress seed data marker exists. Skipping WordPress data seed.");
            return;
        }

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
            => WordPressDataSeeder.ToStoredMediaAsync(mediaSeed, objectStorageService, logger, cancellationToken);

        var existingNewsKeys = await db.News
            .Select(x => new
            {
                x.TitleAz,
                x.CreateDate
            })
            .ToListAsync(cancellationToken);

        var existingNews = existingNewsKeys
            .Select(x => BuildSeedIdentity(x.TitleAz, x.CreateDate))
            .ToHashSet(StringComparer.Ordinal);

        foreach (var item in seed.News)
        {
            var titleAz = Required(item.TitleAz, "Xəbər");
            var created = ParseDate(item.Created);
            var identity = BuildSeedIdentity(titleAz, created);

            if (existingNews.Contains(identity))
                continue;

            var news = new News(
                await ToStoredMediaAsync(item.TitleImage),
                titleAz,
                Required(item.TitleEn, titleAz),
                Required(item.TitleRu, titleAz),
                Required(item.TextAz, titleAz),
                Required(item.TextEn, item.TextAz, titleAz),
                Required(item.TextRu, item.TextAz, titleAz));

            Set(news, nameof(News.CreateDate), created);
            db.News.Add(news);

            foreach (var image in item.Images)
                db.Set<NewsImage>().Add(new NewsImage(await ToStoredMediaAsync(image), news.Id));

            existingNews.Add(identity);
            added++;
        }

        added += await AddMissingAsync(db,
            seed.Announcements,
            (await db.Announcements
                .Select(x => new { x.TitleAz, x.Created })
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x.TitleAz, x.Created))
                .ToHashSet(StringComparer.Ordinal),
            item => BuildSeedIdentity(Required(item.TitleAz, "Elan"), ParseDate(item.Created)),
            async item =>
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
                return announcement;
            });

        added += await AddMissingAsync(db,
            seed.Exhibitions,
            await GetExistingEventIdentitiesAsync<Exhibition>(db, cancellationToken),
            item => BuildSeedIdentity(Required(item.TitleAz, "Sərgi"), ParseDate(item.Created)),
            async item =>
            {
                var entity = new Exhibition(
                Required(item.TitleAz, "Sərgi"),
                Required(item.TitleEn, item.TitleAz, "Exhibition"),
                Required(item.TitleRu, item.TitleAz, "Выставка"),
                await ToStoredMediaAsync(item.TitleImage),
                Required(item.TextAz, item.TitleAz),
                Required(item.TextEn, item.TextAz, item.TitleAz),
                Required(item.TextRu, item.TextAz, item.TitleAz),
                ParseDate(item.StartDate),
                    ParseDate(item.EndDate));
                Set(entity, nameof(Event.Created), ParseDate(item.Created));
                return entity;
            });

        added += await AddMissingAsync(db,
            seed.Trainings,
            await GetExistingEventIdentitiesAsync<Training>(db, cancellationToken),
            item => BuildSeedIdentity(Required(item.TitleAz, "Təlim"), ParseDate(item.Created)),
            async item =>
            {
                var entity = new Training(
                Required(item.TitleAz, "Təlim"),
                Required(item.TitleEn, item.TitleAz, "Training"),
                Required(item.TitleRu, item.TitleAz, "Тренинг"),
                await ToStoredMediaAsync(item.TitleImage),
                Required(item.TextAz, item.TitleAz),
                Required(item.TextEn, item.TextAz, item.TitleAz),
                Required(item.TextRu, item.TextAz, item.TitleAz),
                ParseDate(item.StartDate),
                    ParseDate(item.EndDate));
                Set(entity, nameof(Event.Created), ParseDate(item.Created));
                return entity;
            });

        added += await AddMissingAsync(db,
            seed.BusinessForums,
            (await db.BusinessForums
                .Select(x => x.TitleAz)
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x))
                .ToHashSet(StringComparer.Ordinal),
            item => BuildSeedIdentity(Required(item.TitleAz, "Biznes forum")),
            async item =>
            {
                var eventDate = ParseDate(item.Created);
                return new BusinessForum(
                    Required(item.TitleAz, "Biznes forum"),
                    Required(item.TitleEn, item.TitleAz, "Business forum"),
                    Required(item.TitleRu, item.TitleAz, "Бизнес форум"),
                    await ToStoredMediaAsync(item.TitleImage),
                    Required(item.TextAz, item.TitleAz),
                    Required(item.TextEn, item.TextAz, item.TitleAz),
                    Required(item.TextRu, item.TextAz, item.TitleAz),
                    eventDate,
                    eventDate,
                    await ToStoredMediaAsync(item.DetailImage));
            });

        added += await AddMissingAsync(db,
            seed.Directors,
            (await db.Directors
                .Select(x => new { x.FullNameAz, x.DutyAz })
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x.FullNameAz, x.DutyAz))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.FullNameAz, "Direktor"), Required(x.DutyAz, "-")),
            async x => new Director(
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

        added += await AddMissingAsync(db,
            seed.Management,
            (await db.Management
                .Select(x => new { x.FullNameAz, x.CompanyAz })
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x.FullNameAz, x.CompanyAz))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.FullNameAz, "Üzv"), Required(x.CompanyAz, "-")),
            x => Task.FromResult(new Management(
                Required(x.FullNameAz, "Üzv"),
                Required(x.FullNameEn, x.FullNameAz, "Member"),
                Required(x.FullNameRu, x.FullNameAz, "Член"),
                Required(x.CompanyAz, "-"),
                Required(x.CompanyEn, x.CompanyAz, "-"),
                Required(x.CompanyRu, x.CompanyAz, "-"))));

        added += await AddMissingAsync(db,
            seed.Committees,
            (await db.Committees
                .Select(x => new { x.NameAz, x.ChairmanAz, x.VicePresidentAz })
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x.NameAz, x.ChairmanAz, x.VicePresidentAz))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(
                Required(x.NameAz, "Komissiya"),
                Required(x.ChairmanAz, "-"),
                Required(x.VicePresidentAz, "-")),
            x => Task.FromResult(new Committee(
                Required(x.NameAz, "Komissiya"),
                Required(x.NameEn, x.NameAz, "Committee"),
                Required(x.NameRu, x.NameAz, "Комиссия"),
                Required(x.ChairmanAz, "-"),
                Required(x.ChairmanEn, x.ChairmanAz, "-"),
                Required(x.ChairmanRu, x.ChairmanAz, "-"),
                Required(x.VicePresidentAz, "-"),
                Required(x.VicePresidentEn, x.VicePresidentAz, "-"),
                Required(x.VicePresidentRu, x.VicePresidentAz, "-"))));

        added += await AddMissingAsync(db,
            seed.DistrictRepresentatives,
            (await db.DistrictRepresentatives
                .Select(x => new { x.DistrictAz, x.FullNameAz, x.CompanyAz })
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x.DistrictAz, x.FullNameAz, x.CompanyAz))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.DistrictAz, "-"), Required(x.FullNameAz, "-"), Required(x.CompanyAz, "-")),
            x => Task.FromResult(new DistrictRepresentatives(
                Required(x.DistrictAz, "-"),
                Required(x.DistrictEn, x.DistrictAz, "-"),
                Required(x.DistrictRu, x.DistrictAz, "-"),
                Required(x.FullNameAz, "-"),
                Required(x.FullNameEn, x.FullNameAz, "-"),
                Required(x.FullNameRu, x.FullNameAz, "-"),
                Required(x.CompanyAz, "-"),
                Required(x.CompanyEn, x.CompanyAz, "-"),
                Required(x.CompanyRu, x.CompanyAz, "-"))));

        added += await AddMissingAsync(db,
            seed.ForeignRepresentatives,
            (await db.ForeignRepresentatives
                .Select(x => new { x.CountryAz, x.FullNameAz, x.CompanyAz, x.DutyAz })
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x.CountryAz, x.FullNameAz, x.CompanyAz, x.DutyAz))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(
                Required(x.CountryAz, "-"),
                Required(x.FullNameAz, "-"),
                Required(x.CompanyAz, "-"),
                Required(x.DutyAz, "-")),
            x => Task.FromResult(new ForeignRepresentatives(
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

        added += await AddMissingAsync(db,
            seed.Publications,
            (await db.Publications
                .Select(x => x.TitleAz)
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.TitleAz, "Nəşr")),
            async x => new Publication(
                await ToStoredMediaAsync(x.TitleImage),
                Required(x.TitleAz, "Nəşr"),
                Required(x.TitleEn, x.TitleAz, "Publication"),
                Required(x.TitleRu, x.TitleAz, "Публикация"),
                await ToStoredMediaAsync(x.Pdf)));

        added += await BackfillPublicationMediaAsync(db, seed.Publications, ToStoredMediaAsync, logger, cancellationToken);

        added += await AddMissingAsync(db,
            seed.Partners,
            (await db.Partners
                .Select(x => x.Site)
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.Site, "#")),
            async x => new Partner(await ToStoredMediaAsync(x.Image), Required(x.Site, "#")));

        added += await AddMissingAsync(db,
            seed.InternationalSolidarity,
            (await db.InternationalSolidarity
                .Select(x => x.Link)
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.Link, "#")),
            async x => new InternationalSolidarity(Required(x.Link, "#"), await ToStoredMediaAsync(x.Icon)));

        added += await AddMissingAsync(db,
            seed.Galleries,
            (await db.Galleries
                .Select(x => x.ImageUrl.ObjectKey)
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(ToStoredObjectKey(x.Image)),
            async x => new Gallery(await ToStoredMediaAsync(x.Image)));

        added += await AddMissingAsync(db,
            seed.Faqs,
            (await db.FAQs
                .Select(x => new { x.QuestionAz, x.AnswerAz })
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x.QuestionAz, x.AnswerAz))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.QuestionAz, "Sual"), Required(x.AnswerAz, "-")),
            x => Task.FromResult(new FAQ(
                Required(x.QuestionAz, "Sual"),
                Required(x.QuestionEn, x.QuestionAz, "Question"),
                Required(x.QuestionRu, x.QuestionAz, "Вопрос"),
                Required(x.AnswerAz, "-"),
                Required(x.AnswerEn, x.AnswerAz, "-"),
                Required(x.AnswerRu, x.AnswerAz, "-"))));

        if (seed.President is not null)
        {
            added += await AddMissingAsync(db,
                new[] { seed.President },
                (await db.Presidents
                    .Select(x => x.ImageUrl.ObjectKey)
                    .ToListAsync(cancellationToken))
                    .Select(x => BuildSeedIdentity(x))
                    .ToHashSet(StringComparer.Ordinal),
                x => BuildSeedIdentity(ToStoredObjectKey(x.Image)),
                async x => new President(await ToStoredMediaAsync(x.Image), Required(x.Text, "President")));
        }

        added += await AddMissingAsync(db,
            seed.Services,
            (await db.Services
                .Select(x => x.NameAz)
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.NameAz, "Xidmət")),
            async x => new Service(
                await ToStoredMediaAsync(x.Image),
                Required(x.NameAz, "Xidmət"),
                Required(x.NameEn, x.NameAz, "Service"),
                Required(x.NameRu, x.NameAz, "Услуга")));

        added += await AddMissingAsync(db,
            seed.UsefulLinks,
            (await db.UsefulLinks
                .Select(x => new { x.TitleAz, x.Link })
                .ToListAsync(cancellationToken))
                .Select(x => BuildSeedIdentity(x.TitleAz, x.Link))
                .ToHashSet(StringComparer.Ordinal),
            x => BuildSeedIdentity(Required(x.TitleAz, "Link"), Required(x.Link, "#")),
            x => Task.FromResult(new UsefulLink(
                Required(x.TitleAz, "Link"),
                Required(x.TitleEn, x.TitleAz, "Link"),
                Required(x.TitleRu, x.TitleAz, "Link"),
                Required(x.Link, "#"))));

        foreach (var settingSeed in seed.Settings)
        {
            var setting = await db.Settings.FirstOrDefaultAsync(x => x.Key == settingSeed.Key, cancellationToken);
            if (setting?.ValueType == SettingValueType.Text)
                setting.UpdateStringValue(settingSeed.Value);
        }

        if (db.ChangeTracker.HasChanges())
        {
            await db.SaveChangesAsync(cancellationToken);
            logger.LogInformation("WordPress seed completed. Added approximately {Count} root records.", added);
        }

        await WriteMarkerAsync(objectStorageService, SeedDataReadyMarker, logger, cancellationToken);
    }

    private static async Task<HashSet<string>> GetExistingEventIdentitiesAsync<TEntity>(AppDbContext db, CancellationToken cancellationToken)
        where TEntity : Event
    {
        var values = await db.Set<TEntity>()
            .Select(x => new { x.TitleAz, x.Created })
            .ToListAsync(cancellationToken);

        return values
            .Select(x => BuildSeedIdentity(x.TitleAz, x.Created))
            .ToHashSet(StringComparer.Ordinal);
    }

    private static async Task<int> AddMissingAsync<TSeed, TEntity>(
        AppDbContext db,
        IEnumerable<TSeed> items,
        HashSet<string> existing,
        Func<TSeed, string> identityFactory,
        Func<TSeed, Task<TEntity>> factory)
        where TEntity : class
    {
        var count = 0;
        foreach (var item in items)
        {
            var identity = identityFactory(item);
            if (existing.Contains(identity))
                continue;

            db.Set<TEntity>().Add(await factory(item));
            existing.Add(identity);
            count++;
        }

        return count;
    }

    private static async Task<int> BackfillPublicationMediaAsync(
        AppDbContext db,
        IEnumerable<PublicationSeed> items,
        Func<MediaSeed?, Task<StoredFile>> toStoredMediaAsync,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        var updated = 0;

        foreach (var item in items)
        {
            var titleAz = Required(item.TitleAz, "Nəşr");
            var titleEn = Required(item.TitleEn, item.TitleAz, "Publication");
            var titleRu = Required(item.TitleRu, item.TitleAz, "Публикация");

            var publication = await db.Publications.FirstOrDefaultAsync(x =>
                x.TitleAz == titleAz || x.TitleEn == titleEn || x.TitleRu == titleRu,
                cancellationToken);

            if (publication is null)
                continue;

            var changed = false;

            if (ShouldBackfillSeedMedia(publication.TitleImageUrl.ObjectKey))
            {
                publication.UpdateTitleImage(await toStoredMediaAsync(item.TitleImage));
                changed = true;
            }

            if (ShouldBackfillSeedMedia(publication.PdfUrl.ObjectKey))
            {
                publication.UpdatePdf(await toStoredMediaAsync(item.Pdf));
                changed = true;
            }

            if (changed)
                updated++;
        }

        if (updated > 0)
            logger.LogInformation("WordPress seed backfilled media for {Count} existing publications.", updated);

        return updated;
    }

    private static async Task<bool> MarkerExistsAsync(
        IObjectStorageService objectStorageService,
        string markerName,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        try
        {
            var marker = await objectStorageService.GetAsync(markerName, cancellationToken);
            if (marker is null)
                return false;

            await marker.Stream.DisposeAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Could not check WordPress seed marker {MarkerName}. Seed will continue.", markerName);
            return false;
        }
    }

    private static async Task WriteMarkerAsync(
        IObjectStorageService objectStorageService,
        string markerName,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        try
        {
            var markerText = DateTime.UtcNow.ToString("O");
            await using var marker = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(markerText));
            await objectStorageService.UploadAsync(marker, markerName, "text/plain", marker.Length, cancellationToken);
            logger.LogInformation("WordPress seed marker {MarkerName} was written.", markerName);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Could not write WordPress seed marker {MarkerName}. Seed is idempotent and may retry on next startup.", markerName);
        }
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

        var relativePath = url[SeedMediaPrefix.Length..].Trim().TrimStart('/');
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

    private static bool ShouldBackfillSeedMedia(string? objectKey)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
            return true;

        if (objectKey.StartsWith(MinioSeedPrefix, StringComparison.OrdinalIgnoreCase))
            return false;

        if (objectKey.StartsWith("images/", StringComparison.OrdinalIgnoreCase) ||
            objectKey.StartsWith("documents/", StringComparison.OrdinalIgnoreCase))
            return false;

        if (objectKey.StartsWith(SeedMediaPrefix, StringComparison.OrdinalIgnoreCase))
            return true;

        if (Uri.TryCreate(objectKey, UriKind.Absolute, out var absoluteUri))
        {
            return absoluteUri.Host.Contains("cloudinary", StringComparison.OrdinalIgnoreCase) ||
                   absoluteUri.Host.Contains("ask.org.az", StringComparison.OrdinalIgnoreCase);
        }

        return !objectKey.Contains('/');
    }

    private static DateTime ParseDate(string? value)
        => DateTime.TryParse(value, out var date) ? date : DateTime.UtcNow;

    private static string Required(params string?[] values)
        => values.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x))?.Trim() ?? "-";

    private static string BuildSeedIdentity(string titleAz, DateTime created)
        => BuildSeedIdentity(titleAz, created.ToString("O"));

    private static string BuildSeedIdentity(params string?[] values)
        => string.Join('|', values.Select(value => (value ?? string.Empty).Trim().ToUpperInvariant()));

    private static string ToStoredObjectKey(MediaSeed? mediaSeed)
    {
        var url = Required(mediaSeed?.Url, FallbackImage);

        if (!url.StartsWith(SeedMediaPrefix, StringComparison.OrdinalIgnoreCase))
            return url;

        var relativePath = url[SeedMediaPrefix.Length..].Trim().TrimStart('/');
        return $"{MinioSeedPrefix}{relativePath}";
    }

    private static void Set<T>(T entity, string propertyName, object value)
    {
        var type = entity!.GetType();

        while (type is not null)
        {
            var property = type.GetProperty(
                propertyName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            if (property is not null)
            {
                var setter = property.GetSetMethod(nonPublic: true);
                if (setter is not null)
                {
                    setter.Invoke(entity, [value]);
                    return;
                }

                var backingField = type.GetField(
                    $"<{propertyName}>k__BackingField",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

                backingField?.SetValue(entity, value);
                return;
            }

            type = type.BaseType;
        }
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
