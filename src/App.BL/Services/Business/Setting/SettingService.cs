using App.BL.DTOs;
using App.BL.Mapper.Setting;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Enums;
using App.Core.Interfaces.Repository.Settings;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Setting;

public class SettingService(
    ISettingReadRepository readRepository,
    ISettingWriteRepository writeRepository,
    IStorageService storageService,
    ISettingMapper mapper)
    : StorageEntityService(storageService), ISettingService  
{
    public async Task<Response<IEnumerable<SettingResponseDto>>> GetAllAsync(
        CancellationToken ct = default)
    {
        var entities = await readRepository.GetAllAsync(false, ct);
        var result = entities.Select(mapper.DomainToResponseDto);
        return Response<IEnumerable<SettingResponseDto>>
            .Success(result, $"{result.Count()} settings retrieved successfully");
    }

    public async Task<Response<SettingResponseDto?>> GetByKeyAsync(
        string key, CancellationToken ct = default)
    {
        var entity = await readRepository.GetAsync(s => s.Key == key, false, ct);
        if (entity is null)
            return Response<SettingResponseDto?>.NotFound($"Setting '{key}' tapılmadı");

        return Response<SettingResponseDto?>.Success(
            mapper.DomainToResponseDto(entity), "Uğurla əldə edildi");
    }

    public async Task<Response<SettingResponseDto?>> UpdateAsync(
        string key, UpdateSettingDto dto, CancellationToken ct = default)
    {
        var entity = await readRepository.GetAsync(s => s.Key == key, true, ct);
        if (entity is null)
            return Response<SettingResponseDto?>.NotFound($"Setting '{key}' tapılmadı");

        if (entity.ValueType == SettingValueType.Text)
        {
            if (string.IsNullOrWhiteSpace(dto.Value))
                return Response<SettingResponseDto?>.BadRequest("Dəyər boş ola bilməz.");

            entity.UpdateStringValue(dto.Value.Trim());
        }
        else // Link → MinIO
        {
            if (dto.File is null)
                return Response<SettingResponseDto?>.BadRequest("Fayl mütləq yüklənməlidir.");

            string? oldPublicId = entity.MediaValue?.ObjectKey;

            StoredFile newUrl = await storageService.UploadAsync(dto.File);
            entity.UpdateMediaValue(newUrl);

            await DeleteFileAsync(oldPublicId); 
        }

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(ct);

        return Response<SettingResponseDto?>.Success(
            mapper.DomainToResponseDto(entity), "Uğurla yeniləndi");
    }

    public async Task<Response> DeleteAsync(
        string key, CancellationToken ct = default)
    {
        var entity = await readRepository.GetAsync(s => s.Key == key, true, ct);
        if (entity is null)
            return Response.NotFound($"Setting '{key}' tapılmadı");

        if (entity.ValueType == SettingValueType.Text)
        {
            entity.UpdateStringValue(null);
        }
        else // Link → MinIO
        {
            string? publicId = entity.MediaValue?.ObjectKey;
            entity.UpdateMediaValue(null);
            await DeleteFileAsync(publicId); 
        }

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(ct);

        return Response.Success("Setting dəyəri silindi");
    }
}

