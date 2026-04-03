using App.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities;

internal class BusinessForum : BaseEntity
{
    public string TitleImageUrl { get; set; }
    public string TitleAz { get; set; }
    public string TitleEn { get; set; }
    public string TitleRu { get; set; }
    public DateTime CreateDate { get; set; }
    public string TextAz { get; set; }
    public string TextEn { get; set; }
    public string TextRu { get; set; }
    public string DetailImageUrl { get; set; }
    public BusinessForum(Guid id) : base(id)
    {
    }
}
