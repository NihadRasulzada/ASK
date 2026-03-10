using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.DTOs;

public record CreateAnnouncementDto(string Title, IFormFile TitleImage, string Text);
public record UpdateAnnouncementDto(Guid Id, string Title, IFormFile TitleImage, string Text);
public record AnnouncementResponseDto(Guid Id, string Title, string TitleImageUrl, string Text);