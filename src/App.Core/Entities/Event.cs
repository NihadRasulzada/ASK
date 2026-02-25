using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities
{
    public class Event : AuditableEntity
    {
        public string Title { get; set; }
        public string TitleImageUrl { get; set; }
        public string Text { get; set; }
    }
}
