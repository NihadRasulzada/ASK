namespace App.Core.Entities;

public class Training : Event
{
    // EF Core materialization
    private Training() { }

    public Training(string title, string titleImageUrl, string text)
        : base(title, titleImageUrl, text) { }
}