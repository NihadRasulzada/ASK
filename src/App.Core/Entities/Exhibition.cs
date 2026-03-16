namespace App.Core.Entities;

public class Exhibition : Event
{
    // EF Core materialization
    private Exhibition() { }

    public Exhibition(string title, string titleImageUrl, string text)
        : base(title, titleImageUrl, text) { }
}