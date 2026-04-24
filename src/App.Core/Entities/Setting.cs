using System.ComponentModel.DataAnnotations.Schema;
using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;
using App.Core.Enums;

namespace App.Core.Entities;


public class Setting : BaseEntity
{
    public string Key { get; private set; }
    public string? StringValue { get; private set; }
    public CloudinaryURL? CloudinaryValue { get; private set; }
    public SettingValueType ValueType { get; private set; }

    private Setting() : base(Guid.Empty) { Key = string.Empty; }

    public Setting(Guid id, string key, SettingValueType valueType) : base(id)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Açar boş ola bilməz.", nameof(key));

        Key = key;
        ValueType = valueType;
    }

    public void UpdateStringValue(string? value)
    {
        if (ValueType != SettingValueType.Text)
            throw new InvalidOperationException("Bu setting text tipli deyil.");

        StringValue = value;
    }

    public void UpdateCloudinaryValue(CloudinaryURL? value)
    {
        if (ValueType != SettingValueType.Link)
            throw new InvalidOperationException("Bu setting link tipli deyil.");

        CloudinaryValue = value;
    }
}
