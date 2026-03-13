using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public record CreateTrainingDto(string Title, string Text, IFormFile Image);

public record UpdateTrainingDto(string Title, string Text, IFormFile? Image);

public record TrainingResponseDto(Guid Id, string Title, string Text, string TitleImageUrl, bool IsDeactive);
