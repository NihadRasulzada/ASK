using App.Core.Entities.Common;
using App.Core.Enums;

namespace App.Core.Entities;

public class Setting : BaseEntity
{
    public string Key { get; private set; }
    public string DisplayName { get; private set; }
    public string? Value { get; private set; }
    public SettingValueType ValueType { get; private set; }

    // EF Core materialization
    private Setting() : base(Guid.Empty)
    {
        Key = string.Empty;
        DisplayName = string.Empty;
    }

    /// <summary>
    /// Seed üçün istifadə olunan konstruktor — sabit Guid tələb edir.
    /// </summary>
    public Setting(Guid id, string key, string displayName, SettingValueType valueType)
        : base(id)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Açar boş ola bilməz.", nameof(key));
        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException("Göstərilən ad boş ola bilməz.", nameof(displayName));

        Key = key;
        DisplayName = displayName;
        ValueType = valueType;
        Value = null;
    }

    public void UpdateValue(string? value)
    {
        Value = value;
    }
}
