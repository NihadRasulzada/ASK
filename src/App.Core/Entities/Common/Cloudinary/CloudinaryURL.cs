using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Common.Cloudinary;

public class CloudinaryURL
{
    public string ImageURl { get; private set; }
    public string PublicId { get; private set; }
    
    public CloudinaryURL(string imageurl, string publicId)
    {
        if (string.IsNullOrWhiteSpace(imageurl))
            throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageurl));
        if (string.IsNullOrWhiteSpace(publicId))
            throw new ArgumentException("Public ID cannot be null or empty.", nameof(publicId));

        ImageURl = imageurl;
        PublicId = publicId;
        
    }
    private CloudinaryURL() { }
    public static bool IsNullOrEmpty(CloudinaryURL? cloudinaryUrl)
        => cloudinaryUrl is null ||
           string.IsNullOrWhiteSpace(cloudinaryUrl.ImageURl) ||
           string.IsNullOrWhiteSpace(cloudinaryUrl.PublicId);


}
