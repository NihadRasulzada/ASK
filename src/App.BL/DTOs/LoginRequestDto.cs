namespace App.BL.DTOs;

public record LoginRequestDto(
    string Username,
    string Password
);

public record RefreshTokenRequestDto(
    string Username,
    string RefreshToken
);

public record AuthResponseDto(
    string Token,
    string RefreshToken
);

public record ChangePasswordRequestDto(
    string CurrentPassword,
    string NewPassword,
    string ConfirmPassword
);


