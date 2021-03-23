using System;
using Foundation;
using MovieTime.Data.Services;
using SDWebImage;
using UIKit;

namespace MovieTime.Manager_iOS
{
    public class Manager
    {
        public static void SetPosterImage(UIImageView image, string path)
        {
            image.SetImage(new NSUrl(
                MovieService.GetImageUrl(path)),
                null,
                SDWebImageOptions.RefreshCached);
        }
    }
}
