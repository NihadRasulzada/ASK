using System.Security.Claims;
using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Authentication əməliyyatlarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// İstifadəçini login edir və JWT token qaytarır.
    /// </summary>
    /// <param name="dto">Login məlumatları</param>
    /// <returns>Token və refresh token</returns>
    /// <response code="200">Login uğurludur</response>
    /// <response code="400">Yanlış istifadəçi adı və ya şifrə</response>
    /// <response code="500">Server xətası</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(SuccessResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login( LoginRequestDto dto)
    {
        var response = await userService.LoginAsync(dto);
        return this.HandleServiceResponse(response);
    }


    /// <summary>
    /// Refresh token vasitəsilə yeni access token yaradır.
    /// </summary>
    /// <param name="dto">Refresh token məlumatları</param>
    /// <returns>Yeni token cütü</returns>
    /// <response code="200">Token yeniləndi</response>
    /// <response code="400">Refresh token yanlışdır</response>
    /// <response code="500">Server xətası</response>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(SuccessResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh( RefreshTokenRequestDto dto)
    {
        var response = await userService.RefreshTokenAsync(dto);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// İstifadəçinin şifrəsini dəyişir.
    /// </summary>
    /// <param name="dto">Köhnə və yeni şifrə məlumatları</param>
    /// <returns>Əməliyyat nəticəsi</returns>
    /// <response code="200">Şifrə uğurla dəyişdirildi</response>
    /// <response code="400">Yanlış cari şifrə</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Server xətası</response>
    [HttpPost("change-password")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePassword( ChangePasswordRequestDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var response = await userService.ChangePasswordAsync(userId, dto);

        return this.HandleServiceResponse(response);
    }
}
