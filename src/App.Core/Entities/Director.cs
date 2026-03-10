using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Director : SoftDeletableEntity
{
    public string ImageUrl { get; private set; }
    public string FullName { get; private set; }
    public string Duty { get; private set; }

    private Director() : base(Guid.Empty, false)
    {
        ImageUrl = string.Empty;
        FullName = string.Empty;
        Duty = string.Empty;
    }

    public Director(string imageUrl, string fullName, string duty) : base(Guid.NewGuid(), false)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(duty))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(duty));

        ImageUrl = imageUrl;
        FullName = fullName;
        Duty = duty;
    }

    public void Update(string? imageUrl, string fullName, string duty)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(duty))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(duty));

        if (imageUrl is not null)
            ImageUrl = imageUrl;

        FullName = fullName;
        Duty = duty;
    }
}
