using System;
using Foundation;

namespace SDWebImageSampleMac.DataModel
{
    [Register("ImageModel")]
    public class ImageModel : NSObject
    {
        public string ImageUrl
        {
            get;
            set;
        }

        public ImageModel()
        {

        }

        public ImageModel (string url)
        {
            this.ImageUrl = url;
        }
    }
}
