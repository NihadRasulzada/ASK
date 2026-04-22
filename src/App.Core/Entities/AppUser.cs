using Microsoft.AspNetCore.Identity;

namespace App.Core.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}



