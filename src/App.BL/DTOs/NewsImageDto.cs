using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.DTOs;

public record UpdateNewsImageDto(Guid Id, IFormFile Image);
public record CreateNewsImageDto(IFormFile Image, Guid NewsId);
public record NewsImageResponseDto(Guid Id, string ImageUrl);