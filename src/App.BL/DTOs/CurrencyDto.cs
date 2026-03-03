namespace App.BL.DTOs;

/// <summary>
/// Client-ə qaytarılan valyuta məzənnəsi məlumatı.
/// </summary>
/// <param name="Code">Valyuta kodu (məs. EUR, USD).</param>
/// <param name="Rate">Azərbaycanca manata nisbətən cari məzənnə.</param>
/// <param name="ChangePercent">24 saat ərzindəki dəyişiklik faizi.</param>
public record CurrencyRateDto(
    string Code,
    decimal Rate,
    decimal ChangePercent);
