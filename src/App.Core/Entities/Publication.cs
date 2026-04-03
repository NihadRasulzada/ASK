using App.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities;

internal class Publication : BaseEntity
{
    public string TitleImageUrl { get; set; }
    public string TitleAz { get; set; }
    public string TitleEn { get; set; }
    public string TitleRu { get; set; }
    public string PdfUrl { get; set; }
    public Publication(Guid id) : base(id)
    {
    }
}
