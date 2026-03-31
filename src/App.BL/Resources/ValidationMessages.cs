namespace App.BL.Resources;

public static class ValidationMessages
{
    // ── General ───────────────────────────────────────────────────────────────
    public static string ValidationFailed(string lang) => lang switch
    {
        "en" => "Validation failed.",
        "ru" => "Ошибка валидации.",
        _ => "Validasiya xətası."
    };

    public static string UnexpectedError(string lang) => lang switch
    {
        "en" => "An unexpected error occurred.",
        "ru" => "Произошла непредвиденная ошибка.",
        _ => "Gözlənilməz xəta baş verdi."
    };

    public static string IdRequired(string lang) => lang switch
    {
        "en" => "Id is required.",
        "ru" => "Идентификатор обязателен.",
        _ => "Id mütləq daxil edilməlidir."
    };

    public static string NewsIdRequired(string lang) => lang switch
    {
        "en" => "News Id is required.",
        "ru" => "Идентификатор новости обязателен.",
        _ => "Xəbər Id-si mütləq daxil edilməlidir."
    };

    // ── Image ─────────────────────────────────────────────────────────────────
    public static string ImageRequired(string lang) => lang switch
    {
        "en" => "Image is required.",
        "ru" => "Изображение обязательно.",
        _ => "Şəkil mütləq daxil edilməlidir."
    };

    public static string ImageTooLarge(string lang) => lang switch
    {
        "en" => "Image size cannot exceed 5 MB.",
        "ru" => "Размер изображения не может превышать 5 МБ.",
        _ => "Şəklin ölçüsü 5 MB-dan çox ola bilməz."
    };

    public static string ImageInvalidFormat(string lang) => lang switch
    {
        "en" => "Only JPEG, PNG, GIF or WebP images are allowed.",
        "ru" => "Допускаются только изображения JPEG, PNG, GIF или WebP.",
        _ => "Yalnız JPEG, PNG, GIF və ya WebP formatında şəkil yüklənə bilər."
    };

    // ── Service ───────────────────────────────────────────────────────────────
    public static string ServiceNameAzRequired(string lang) => lang switch
    {
        "en" => "Azerbaijani name is required.",
        "ru" => "Азербайджанское название обязательно.",
        _ => "Az dili adı mütləq daxil edilməlidir."
    };

    public static string ServiceNameAzTooLong(string lang) => lang switch
    {
        "en" => "Azerbaijani name cannot exceed 200 characters.",
        "ru" => "Азербайджанское название не может превышать 200 символов.",
        _ => "Az dili adı 200 simvoldan çox ola bilməz."
    };

    public static string ServiceNameEnRequired(string lang) => lang switch
    {
        "en" => "English name is required.",
        "ru" => "Английское название обязательно.",
        _ => "En dili adı mütləq daxil edilməlidir."
    };

    public static string ServiceNameEnTooLong(string lang) => lang switch
    {
        "en" => "English name cannot exceed 200 characters.",
        "ru" => "Английское название не может превышать 200 символов.",
        _ => "En dili adı 200 simvoldan çox ola bilməz."
    };

    public static string ServiceNameRuRequired(string lang) => lang switch
    {
        "en" => "Russian name is required.",
        "ru" => "Русское название обязательно.",
        _ => "Ru dili adı mütləq daxil edilməlidir."
    };

    public static string ServiceNameRuTooLong(string lang) => lang switch
    {
        "en" => "Russian name cannot exceed 200 characters.",
        "ru" => "Русское название не может превышать 200 символов.",
        _ => "Ru dili adı 200 simvoldan çox ola bilməz."
    };

    // ── Director ──────────────────────────────────────────────────────────────
    public static string FullNameRequired(string lang) => lang switch
    {
        "en" => "Full name is required.",
        "ru" => "Полное имя обязательно.",
        _ => "Tam ad mütləq daxil edilməlidir."
    };

    public static string FullNameTooLong(string lang) => lang switch
    {
        "en" => "Full name cannot exceed 200 characters.",
        "ru" => "Полное имя не может превышать 200 символов.",
        _ => "Tam ad 200 simvoldan çox ola bilməz."
    };

    public static string DutyRequired(string lang) => lang switch
    {
        "en" => "Duty is required.",
        "ru" => "Должность обязательна.",
        _ => "Vəzifə mütləq daxil edilməlidir."
    };

    public static string DutyTooLong(string lang) => lang switch
    {
        "en" => "Duty cannot exceed 200 characters.",
        "ru" => "Должность не может превышать 200 символов.",
        _ => "Vəzifə 200 simvoldan çox ola bilməz."
    };

    public static string PhoneNumberTooLong(string lang) => lang switch
    {
        "en" => "Phone number cannot exceed 50 characters.",
        "ru" => "Номер телефона не может превышать 50 символов.",
        _ => "Telefon nömrəsi 50 simvoldan çox ola bilməz."
    };

    public static string PhoneNumberInvalidFormat(string lang) => lang switch
    {
        "en" => "Phone number format is invalid.",
        "ru" => "Неверный формат номера телефона.",
        _ => "Telefon nömrəsinin formatı yanlışdır."
    };

    public static string EmailInvalidFormat(string lang) => lang switch
    {
        "en" => "Email address is invalid.",
        "ru" => "Неверный формат электронной почты.",
        _ => "E-poçt ünvanı düzgün deyil."
    };

    public static string EmailTooLong(string lang) => lang switch
    {
        "en" => "Email address cannot exceed 256 characters.",
        "ru" => "Электронная почта не может превышать 256 символов.",
        _ => "E-poçt ünvanı 256 simvoldan çox ola bilməz."
    };

    // ── News ──────────────────────────────────────────────────────────────────
    public static string TitleImageRequired(string lang) => lang switch
    {
        "en" => "Title image is required.",
        "ru" => "Обложка обязательна.",
        _ => "Başlıq şəkli mütləq daxil edilməlidir."
    };

    public static string TitleImageTooLarge(string lang) => lang switch
    {
        "en" => "Title image size cannot exceed 5 MB.",
        "ru" => "Размер обложки не может превышать 5 МБ.",
        _ => "Başlıq şəklinin ölçüsü 5 MB-dan çox ola bilməz."
    };

    public static string NewsTextRequired(string lang) => lang switch
    {
        "en" => "News text is required.",
        "ru" => "Текст новости обязателен.",
        _ => "Xəbər mətni mütləq daxil edilməlidir."
    };

    public static string NewsTextTooLong(string lang) => lang switch
    {
        "en" => "News text cannot exceed 10000 characters.",
        "ru" => "Текст новости не может превышать 10000 символов.",
        _ => "Xəbər mətni 10000 simvoldan çox ola bilməz."
    };

    public static string AdditionalImageTooLarge(string lang) => lang switch
    {
        "en" => "Each additional image cannot exceed 5 MB.",
        "ru" => "Каждое дополнительное изображение не может превышать 5 МБ.",
        _ => "Hər əlavə şəklin ölçüsü 5 MB-dan çox ola bilməz."
    };

    // ── Partner ───────────────────────────────────────────────────────────────
    public static string SiteRequired(string lang) => lang switch
    {
        "en" => "Site URL is required.",
        "ru" => "URL сайта обязателен.",
        _ => "Sayt URL-i mütləq daxil edilməlidir."
    };

    public static string SiteTooLong(string lang) => lang switch
    {
        "en" => "Site URL cannot exceed 2048 characters.",
        "ru" => "URL сайта не может превышать 2048 символов.",
        _ => "Sayt URL-i 2048 simvoldan çox ola bilməz."
    };

    public static string SiteInvalidUrl(string lang) => lang switch
    {
        "en" => "Site URL must be a valid URL (http or https).",
        "ru" => "URL сайта должен быть корректным (http или https).",
        _ => "Sayt URL-i düzgün bir URL olmalıdır (http və ya https)."
    };

    // ── President ─────────────────────────────────────────────────────────────
    public static string TextRequired(string lang) => lang switch
    {
        "en" => "Text is required.",
        "ru" => "Текст обязателен.",
        _ => "Mətn mütləq daxil edilməlidir."
    };

    public static string TextTooLong(string lang) => lang switch
    {
        "en" => "Text cannot exceed 5000 characters.",
        "ru" => "Текст не может превышать 5000 символов.",
        _ => "Mətn 5000 simvoldan çox ola bilməz."
    };

    // ── Video ─────────────────────────────────────────────────────────────────
    public static string LinkRequired(string lang) => lang switch
    {
        "en" => "Link is required.",
        "ru" => "Ссылка обязательна.",
        _ => "Link mütləq daxil edilməlidir."
    };

    public static string LinkTooLong(string lang) => lang switch
    {
        "en" => "Link cannot exceed 2048 characters.",
        "ru" => "Ссылка не может превышать 2048 символов.",
        _ => "Link 2048 simvoldan çox ola bilməz."
    };

    public static string LinkInvalidUrl(string lang) => lang switch
    {
        "en" => "Link must be a valid URL (http or https).",
        "ru" => "Ссылка должна быть корректным URL (http или https).",
        _ => "Link düzgün bir URL olmalıdır (http və ya https)."
    };

    // ── Title ─────────────────────────────────────────────────────────────────
    public static string TitleRequired(string lang) => lang switch
    {
        "en" => "Title is required.",
        "ru" => "Заголовок обязателен.",
        _ => "Başlıq mütləq daxil edilməlidir."
    };

    public static string TitleTooLong(string lang) => lang switch
    {
        "en" => "Title cannot exceed 500 characters.",
        "ru" => "Заголовок не может превышать 500 символов.",
        _ => "Başlıq 500 simvoldan çox ola bilməz."
    };
}
