using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.NewsImages.Business.NewsIamge;

public interface INewsImageService
{
    Task<Response<IEnumerable<NewsImageResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<NewsImageResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> CreateAsync(CreateNewsImageDto dto, CancellationToken cancellationToken = default);
    Task<Response<NewsImageResponseDto?>> UpdateAsync(Guid id,UpdateNewsImageDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}