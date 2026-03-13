using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public record CreateExhibitionDto(string Title, string Text, IFormFile Image);

public record UpdateExhibitionDto(string Title, string Text, IFormFile? Image);

public record ExhibitionResponseDto(Guid Id, string Title, string Text, string TitleImageUrl, bool IsDeactive);
