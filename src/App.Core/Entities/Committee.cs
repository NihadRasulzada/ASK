using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Committee : BaseEntity
{
    public string NameAz { get; private set; }
    public string NameEn { get; private set; }
    public string NameRu { get; private set; }
    public string ChairmanAz { get; private set; }
    public string ChairmanEn { get; private set; }
    public string ChairmanRu { get; private set; }
    public string VicePresidentAz { get; private set; }
    public string VicePresidentEn { get; private set; }
    public string VicePresidentRu { get; private set; }

    private Committee() : base(Guid.Empty)
    {
        NameAz = string.Empty;
        NameEn = string.Empty;
        NameRu = string.Empty;
        ChairmanAz = string.Empty;
        ChairmanEn = string.Empty;
        ChairmanRu = string.Empty;
        VicePresidentAz = string.Empty;
        VicePresidentEn = string.Empty;
        VicePresidentRu = string.Empty;
    }

    public Committee(
        string nameAz, string nameEn, string nameRu,
        string chairmanAz, string chairmanEn, string chairmanRu,
        string vicePresidentAz, string vicePresidentEn, string vicePresidentRu)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(nameAz)) throw new ArgumentException("Ad (AZ) boş ola bilməz.", nameof(nameAz));
        if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("Ad (EN) boş ola bilməz.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameRu)) throw new ArgumentException("Ad (RU) boş ola bilməz.", nameof(nameRu));
        if (string.IsNullOrWhiteSpace(chairmanAz)) throw new ArgumentException("Sədrin adı (AZ) boş ola bilməz.", nameof(chairmanAz));
        if (string.IsNullOrWhiteSpace(chairmanEn)) throw new ArgumentException("Sədrin adı (EN) boş ola bilməz.", nameof(chairmanEn));
        if (string.IsNullOrWhiteSpace(chairmanRu)) throw new ArgumentException("Sədrin adı (RU) boş ola bilməz.", nameof(chairmanRu));
        if (string.IsNullOrWhiteSpace(vicePresidentAz)) throw new ArgumentException("Vitse-prezidentin adı (AZ) boş ola bilməz.", nameof(vicePresidentAz));
        if (string.IsNullOrWhiteSpace(vicePresidentEn)) throw new ArgumentException("Vitse-prezidentin adı (EN) boş ola bilməz.", nameof(vicePresidentEn));
        if (string.IsNullOrWhiteSpace(vicePresidentRu)) throw new ArgumentException("Vitse-prezidentin adı (RU) boş ola bilməz.", nameof(vicePresidentRu));

        NameAz = nameAz;
        NameEn = nameEn;
        NameRu = nameRu;
        ChairmanAz = chairmanAz;
        ChairmanEn = chairmanEn;
        ChairmanRu = chairmanRu;
        VicePresidentAz = vicePresidentAz;
        VicePresidentEn = vicePresidentEn;
        VicePresidentRu = vicePresidentRu;
    }

    public void Update(
        string nameAz, string nameEn, string nameRu,
        string chairmanAz, string chairmanEn, string chairmanRu,
        string vicePresidentAz, string vicePresidentEn, string vicePresidentRu)
    {
        if (string.IsNullOrWhiteSpace(nameAz)) throw new ArgumentException("Ad (AZ) boş ola bilməz.", nameof(nameAz));
        if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("Ad (EN) boş ola bilməz.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameRu)) throw new ArgumentException("Ad (RU) boş ola bilməz.", nameof(nameRu));
        if (string.IsNullOrWhiteSpace(chairmanAz)) throw new ArgumentException("Sədrin adı (AZ) boş ola bilməz.", nameof(chairmanAz));
        if (string.IsNullOrWhiteSpace(chairmanEn)) throw new ArgumentException("Sədrin adı (EN) boş ola bilməz.", nameof(chairmanEn));
        if (string.IsNullOrWhiteSpace(chairmanRu)) throw new ArgumentException("Sədrin adı (RU) boş ola bilməz.", nameof(chairmanRu));
        if (string.IsNullOrWhiteSpace(vicePresidentAz)) throw new ArgumentException("Vitse-prezidentin adı (AZ) boş ola bilməz.", nameof(vicePresidentAz));
        if (string.IsNullOrWhiteSpace(vicePresidentEn)) throw new ArgumentException("Vitse-prezidentin adı (EN) boş ola bilməz.", nameof(vicePresidentEn));
        if (string.IsNullOrWhiteSpace(vicePresidentRu)) throw new ArgumentException("Vitse-prezidentin adı (RU) boş ola bilməz.", nameof(vicePresidentRu));

        NameAz = nameAz;
        NameEn = nameEn;
        NameRu = nameRu;
        ChairmanAz = chairmanAz;
        ChairmanEn = chairmanEn;
        ChairmanRu = chairmanRu;
        VicePresidentAz = vicePresidentAz;
        VicePresidentEn = vicePresidentEn;
        VicePresidentRu = vicePresidentRu;
    }
}
