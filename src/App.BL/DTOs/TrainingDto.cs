using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxladığı üçün class (record deyil)
public class CreateTrainingDto
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
}

public class UpdateTrainingDto
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}

public record TrainingResponseDto(Guid Id, string Title, string Text, string TitleImageUrl, bool IsDeactive);