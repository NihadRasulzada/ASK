namespace App.BL.DTOs;

public class CreateUsefulLinkDto
{
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}

public class UpdateUsefulLinkDto
{
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}

public record UsefulLinkResponseDto(Guid Id, string Title, string Link, bool IsDeactive);
