using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace crop
{
    public class ImageClass
    {
        //图片裁剪
        public static Image<Bgr, Byte> Cut(Image<Bgr, Byte> image, Rectangle rectangle)
        {
            Image<Bgr, byte> CropImage = new Image<Bgr, byte>(rectangle.Size);
            CvInvoke.cvCopy(image, CropImage, IntPtr.Zero);

            return CropImage;
        }
    }
}
