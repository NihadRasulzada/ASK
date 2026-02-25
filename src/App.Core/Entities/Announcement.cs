using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities
{
    public class Announcement : AuditableEntity
    {
        public string TitleImageUrl { get; set; }
        public string Text { get; set; }
    }
}
