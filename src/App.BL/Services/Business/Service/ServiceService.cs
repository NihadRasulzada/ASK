using App.BL.DTOs;
using App.BL.Mapper.Service;
using App.BL.Services.External;
using App.Core.Interfaces;
using App.Core.Interfaces.Repository.Service;
using App.Core.ResponseObject.Concreate;
using App.DAL.Context;

namespace App.BL.Services.Business.Service;

public class ServiceService(
    IServiceReadRepository readRepository,
    IServiceWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    ILanguageService languageService,
    IServiceMapper serviceMapper,
    AppDbContext context) : IServiceService
{
    public async Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Service? service = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (service == null)
        {
            //TODO: Dil desteyi
            return Response.NotFound("Service not found");
        }

        if (service.IsDeactive == false)
        {
            return Response.BadRequest("Service is already active");
        }

        IEnumerable<Core.Entities.Service> services = await readRepository.GetAllIncludingDeletedAsync(cancellationToken, predicate: x => !x.IsDeactive, orderBy: q => q.OrderBy(x => x.ActivateAt));

        if (services.Count() >= 7)
        {
            services.First().Deactivate();
        }

        service.Activate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Service activated successfully");
    }

    public async Task<Response> CreateAsync(CreateServiceDto dto, CancellationToken cancellationToken = default)
    {
        string imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
        Core.Entities.Service newService = serviceMapper.CreateDtoToDomain(dto, imageUrl);

        IEnumerable<Core.Entities.Service> services = await readRepository.GetAllIncludingDeletedAsync(cancellationToken, predicate: x => !x.IsDeactive, orderBy: q => q.OrderBy(x => x.ActivateAt));

        if (services.Count() >= 7)
        {
            services.First().Deactivate();
        }

        await writeRepository.AddAsync(newService, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Service created successfully");
    }

    public async Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Service? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);

        if (entity == null)
        {
            return Response.NotFound($"Service not found");
        }

        if (entity.IsDeactive)
        {
            return Response.BadRequest($"Service is already deactive");
        }

        entity.Deactivate();
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success($"Service deactivated successfully");

    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Service? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity == null)
        {
            return Response.NotFound($"Service not found");
        }

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success($"Service deleted successfully");

    }

    public async Task<Response<IEnumerable<ServiceResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Service>? entities = await readRepository.GetAllAsync(false, cancellationToken, false);

        if (!entities.Any())
            return Response<IEnumerable<ServiceResponseDto>>.Success(Enumerable.Empty<ServiceResponseDto>(), $"No {nameof(Core.Entities.Service)}s found");

        IEnumerable<ServiceResponseDto> viewModels = entities.Select(t => serviceMapper.DomainToResponseDto(t));

        return Response<IEnumerable<ServiceResponseDto>>.Success(viewModels, $"{entities.Count()} {nameof(Core.Entities.Service)}(s) retrieved successfully");
    }

    public async Task<Response<IEnumerable<ServiceResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Service> entities = await readRepository.GetAllIncludingDeletedAsync(cancellationToken);

        if (!entities.Any())
            return Response<IEnumerable<ServiceResponseDto>>.Success(Enumerable.Empty<ServiceResponseDto>(), $"No {nameof(Core.Entities.Service)}s found");

        IEnumerable<ServiceResponseDto> viewModels = entities.Select(t => serviceMapper.DomainToResponseDto(t));

        return Response<IEnumerable<ServiceResponseDto>>.Success(viewModels, $"{entities.Count()} {nameof(Core.Entities.Service)}(s) retrieved successfully");
    }

    public async Task<Response<ServiceResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Service? entity = await readRepository.GetByIdAsync((Guid)id, false, cancellationToken);

        if (entity == null)
            return Response<ServiceResponseDto?>.NotFound($"{nameof(Core.Entities.Service)} not found");

        ServiceResponseDto viewModel = serviceMapper.DomainToResponseDto(entity);

        return Response<ServiceResponseDto?>.Success(viewModel, $"{nameof(Core.Entities.Service)} retrieved successfully");
    }

    public async Task<Response<ServiceResponseDto?>> UpdateAsync(Guid id,UpdateServiceDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Service? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
        {
            return Response<ServiceResponseDto?>.NotFound($"{nameof(Core.Entities.Service)} not found");
        }

        IEnumerable<Core.Entities.Service> services = await readRepository.GetAllIncludingDeletedAsync(cancellationToken, predicate: x => !x.IsDeactive, orderBy: q => q.OrderBy(x => x.ActivateAt));

        if (services.Count() >= 7)
        {
            services.First().Deactivate();
        }

        Core.Entities.Service newEntity;

        if (dto.Image != null)
        {
            string imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
            newEntity = serviceMapper.UpdateDtoToDamain(entity, dto, imageUrl);
        }
        else
        {
            newEntity = serviceMapper.UpdateDtoToDamain(entity, dto, entity.ImageUrl);
        }


        writeRepository.Update(newEntity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<ServiceResponseDto?>.Success(serviceMapper.DomainToResponseDto(newEntity), $"{nameof(Core.Entities.Service)} updated successfully");
    }
}