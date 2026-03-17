using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.DTOs;

public class UpdateNewsImageDto
{
    public Guid Id { get; set; }
    public IFormFile Image { get; set; }
};
public class CreateNewsImageDto
{
    public Guid NewsId { get; set; }
    public IFormFile Image { get; set; }
};
public record NewsImageResponseDto(Guid Id, string ImageUrl, Guid NewsId);