using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IImageService
    {
        string ReduceImageSize(string path, System.Drawing.Image image, int quality = 0);
    }
}
