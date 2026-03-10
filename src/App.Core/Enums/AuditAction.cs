namespace App.Core.Enums;

public enum AuditAction : byte
{
    Create,

    Update,

    SoftDelete,

    HardDelete,
}