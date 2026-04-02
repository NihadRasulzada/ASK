using App.BL.DTOs;
using App.BL.Mapper.Setting;
using App.BL.Services.External;
using App.Core.Enums;
using App.Core.Interfaces.Repository.Settings;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Setting;

public class SettingService(
    ISettingReadRepository readRepository,
    ISettingWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    ISettingMapper mapper) : ISettingService
{
    public async Task<Response<IEnumerable<SettingResponseDto>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Setting> entities =
            await readRepository.GetAllAsync(false, cancellationToken);

        IEnumerable<SettingResponseDto> result = entities.Select(mapper.DomainToResponseDto);

        return Response<IEnumerable<SettingResponseDto>>
            .Success(result, $"{result.Count()} settings retrieved successfully");
    }

    public async Task<Response<SettingResponseDto?>> GetByKeyAsync(
        string key, CancellationToken cancellationToken = default)
    {
        Core.Entities.Setting? entity = await readRepository.GetAsync(
            s => s.Key == key, false, cancellationToken);

        if (entity is null)
            return Response<SettingResponseDto?>.NotFound($"Setting '{key}' tapılmadı");

        return Response<SettingResponseDto?>.Success(
            mapper.DomainToResponseDto(entity),
            "Setting uğurla əldə edildi");
    }

    public async Task<Response<SettingResponseDto?>> UpdateAsync(
        string key, UpdateSettingDto dto, CancellationToken cancellationToken = default)
    {
        // 1. Tracking ilə yüklə (EF update üçün lazımdır)
        Core.Entities.Setting? entity = await readRepository.GetAsync(
            s => s.Key == key, true, cancellationToken);

        if (entity is null)
            return Response<SettingResponseDto?>.NotFound($"Setting '{key}' tapılmadı");

        // 2. ValueType-a görə dəyəri müəyyənləşdir
        string? resolvedValue;

        if (entity.ValueType == SettingValueType.Link)
        {
            if (dto.PdfFile is null)
                return Response<SettingResponseDto?>.BadRequest(
                    "PDF fayl mütləq yüklənməlidir (link tipli setting).");

            // Content-type + size yoxlaması UploadPdfAsync içindədir
            resolvedValue = await cloudinaryService.UploadPdfAsync(dto.PdfFile);
        }
        else // SettingValueType.Text
        {
            if (string.IsNullOrWhiteSpace(dto.Value))
                return Response<SettingResponseDto?>.BadRequest(
                    "Dəyər boş ola bilməz (text tipli setting).");

            resolvedValue = dto.Value.Trim();
        }

        // 3. Entity-ni yenilə və saxla
        entity.UpdateValue(resolvedValue);
        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<SettingResponseDto?>.Success(
            mapper.DomainToResponseDto(entity),
            "Setting uğurla yeniləndi");
    }
}
