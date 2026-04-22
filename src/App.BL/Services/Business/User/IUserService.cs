using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;


namespace App.BL.Services.Business.User;

public interface IUserService
{
    Task<Response<AuthResponseDto>> LoginAsync(LoginRequestDto dto);
    Task<Response<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);
    Task<Response> ChangePasswordAsync(string userId, ChangePasswordRequestDto dto);

}
