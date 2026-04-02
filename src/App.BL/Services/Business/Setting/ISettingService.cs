using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Setting;

public interface ISettingService
{
    Task<Response<IEnumerable<SettingResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<SettingResponseDto?>> GetByKeyAsync(string key, CancellationToken cancellationToken = default);
    Task<Response<SettingResponseDto?>> UpdateAsync(string key, UpdateSettingDto dto, CancellationToken cancellationToken = default);
}
