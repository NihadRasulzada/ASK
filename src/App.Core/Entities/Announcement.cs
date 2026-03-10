using App.Core.Entities.Common;

namespace App.Core.Entities
{
    public class Announcement : BaseEntity
    {
        public string TitleImageUrl { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Announcement(string title, string titleImageUrl, string text) : base(Guid.NewGuid())
        {
            TitleImageUrl = titleImageUrl;
            Title = title;
            Text = text;
        }

        private Announcement() : base(Guid.Empty)
        {
        }

        public void Update(string title, string? titleImageUrl, string text)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Tam ad boş ola bilməz.", nameof(title));

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(text));

            if (titleImageUrl is not null)
                TitleImageUrl = titleImageUrl;

            Title = title;
            Text = text;
        }

    }
}
