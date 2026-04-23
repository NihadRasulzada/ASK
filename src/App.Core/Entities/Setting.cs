using System.ComponentModel.DataAnnotations.Schema;
using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;
using App.Core.Enums;

namespace App.Core.Entities;

public class Setting : BaseEntity
{
    [NotMapped]
    private CloudinaryURL cloudinaryURL;
    [NotMapped]
    private string? value;
    public string Key { get; private set; }
    public string? Value
    {
        get
        {
            return ValueType switch
            {
                SettingValueType.Text => value ?? string.Empty,
                SettingValueType.Link => cloudinaryURL?.ImageURl ?? string.Empty,
                _ => throw new InvalidOperationException($"Dəyər tipi '{ValueType}' üçün dəstək yoxdur.")
            };
        }
    } 
    public SettingValueType ValueType { get; private set; }

    public Guid? CloudinaryURLId { get; private set; }


    // EF Core materialization
    private Setting() : base(Guid.Empty)
    {
    }

    /// <summary>
    /// Seed üçün istifadə olunan konstruktor — sabit Guid tələb edir.
    /// </summary>
    public Setting(Guid id, string key, SettingValueType valueType)
        : base(id)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Açar boş ola bilməz.", nameof(key));

        Key = key;
        ValueType = valueType;
    }

    public void UpdateValue((string?, CloudinaryURL?) value)
    {
       this.value = value.Item1;
       cloudinaryURL = value.Item2;
    }
}
