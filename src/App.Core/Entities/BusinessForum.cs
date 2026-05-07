using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class BusinessForum : Event
{
    public CloudinaryURL DetailImageUrl { get; private set; }
    public BusinessForum() { }
    public BusinessForum(string titleAz, string titleEn, string titleRu, CloudinaryURL titleImageUrl, string textAz, string textEn, string textRu, DateTime startdate, DateTime enddate, CloudinaryURL detailImageUrl)
        : base(titleAz, titleEn, titleRu, titleImageUrl, textAz, textEn, textRu, startdate, enddate)
    {
        DetailImageUrl = detailImageUrl;
    }

    //public void UpdateTitleImageUrl(CloudinaryURL titleImageUrl)
    //{
    //    if (CloudinaryURL.IsNullOrEmpty(titleImageUrl))
    //        throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

    //    TitleImageUrl = titleImageUrl;
    //}

    public void UpdateDetailImageUrl(CloudinaryURL detailImageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(detailImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(detailImageUrl));

        DetailImageUrl = detailImageUrl;
    }

}
