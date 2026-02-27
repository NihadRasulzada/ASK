namespace App.API.Filters;

public record ValidationErrorResponse(Dictionary<string, string[]> Errors);
