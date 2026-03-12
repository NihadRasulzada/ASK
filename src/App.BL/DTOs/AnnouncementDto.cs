using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public record CreateAnnouncementDto(string Title, IFormFile TitleImage, string Text);
public record UpdateAnnouncementDto(Guid Id, string Title, IFormFile TitleImage, string Text);
public record AnnouncementResponseDto(Guid Id, string Title, string TitleImageUrl, string Text);