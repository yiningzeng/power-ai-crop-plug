using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace crop
{
    //public class OpenCVEmguCVDotNet
    //{
    //    /// <summary>
    //    /// 将MIplImage结构转换到IplImage指针；
    //    /// 注意：指针在使用完之后必须用Marshal.FreeHGlobal方法释放。
    //    /// </summary>
    //    /// <param name="mi">MIplImage对象</param>
    //    /// <returns>返回IplImage指针</returns>
    //    public static IntPtr MIplImageToIplImagePointer(MIplImage mi)
    //    {
    //        IntPtr ptr = Marshal.AllocHGlobal(mi.nSize);
    //        Marshal.StructureToPtr(mi, ptr, false);
    //        return ptr;
    //    }

    //    /// <summary>
    //    /// 将IplImage指针转换成MIplImage结构
    //    /// </summary>
    //    /// <param name="ptr">IplImage指针</param>
    //    /// <returns>返回MIplImage结构</returns>
    //    public static MIplImage IplImagePointerToMIplImage(IntPtr ptr)
    //    {
    //        return (MIplImage)Marshal.PtrToStructure(ptr, typeof(MIplImage));
    //    }

    //    /// <summary>
    //    /// 将IplImage指针转换成Emgucv中的Image对象；
    //    /// 注意：这里需要您自己根据IplImage中的depth和nChannels来决定
    //    /// </summary>
    //    /// <typeparam name="TColor">Color type of this image (either Gray, Bgr, Bgra, Hsv, Hls, Lab, Luv, Xyz or Ycc)</typeparam>
    //    /// <typeparam name="TDepth">Depth of this image (either Byte, SByte, Single, double, UInt16, Int16 or Int32)</typeparam>
    //    /// <param name="ptr">IplImage指针</param>
    //    /// <returns>返回Image对象</returns>
    //    public static Image<TColor, TDepth> IplImagePointerToEmgucvImage<TColor, TDepth>(IntPtr ptr)
    //        where TColor : struct, IColor
    //        where TDepth : new()
    //    {
    //        MIplImage mi = IplImagePointerToMIplImage(ptr);
    //        return new Image<TColor, TDepth>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //    }

    //    /// <summary>
    //    /// 将IplImage指针转换成Emgucv中的IImage接口；
    //    /// 1通道对应灰度图像，3通道对应BGR图像，4通道对应BGRA图像。
    //    /// 注意：3通道可能并非BGR图像，而是HLS,HSV等图像
    //    /// </summary>
    //    /// <param name="ptr">IplImage指针</param>
    //    /// <returns>返回IImage接口</returns>
    //    public static IImage IplImagePointToEmgucvIImage(IntPtr ptr)
    //    {
    //        MIplImage mi = IplImagePointerToMIplImage(ptr);
    //        Type tColor;
    //        Type tDepth;
    //        string unsupportedDepth = "不支持的像素位深度IPL_DEPTH";
    //        string unsupportedChannels = "不支持的通道数（仅支持1，2，4通道）";
    //        switch (mi.nChannels)
    //        {
    //            case 1:
    //                tColor = typeof(Gray);
    //                switch (mi.depth)
    //                {
    //                    case IPL_DEPTH.IPL_DEPTH_8U:
    //                        tDepth = typeof(Byte);
    //                        return new Image<Gray, Byte>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_16U:
    //                        tDepth = typeof(UInt16);
    //                        return new Image<Gray, UInt16>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_16S:
    //                        tDepth = typeof(Int16);
    //                        return new Image<Gray, Int16>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_32S:
    //                        tDepth = typeof(Int32);
    //                        return new Image<Gray, Int32>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_32F:
    //                        tDepth = typeof(Single);
    //                        return new Image<Gray, Single>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_64F:
    //                        tDepth = typeof(Double);
    //                        return new Image<Gray, Double>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    default:
    //                        throw new NotImplementedException(unsupportedDepth);
    //                }
    //            case 3:
    //                tColor = typeof(Bgr);
    //                switch (mi.depth)
    //                {
    //                    case IPL_DEPTH.IPL_DEPTH_8U:
    //                        tDepth = typeof(Byte);
    //                        return new Image<Bgr, Byte>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_16U:
    //                        tDepth = typeof(UInt16);
    //                        return new Image<Bgr, UInt16>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_16S:
    //                        tDepth = typeof(Int16);
    //                        return new Image<Bgr, Int16>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_32S:
    //                        tDepth = typeof(Int32);
    //                        return new Image<Bgr, Int32>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_32F:
    //                        tDepth = typeof(Single);
    //                        return new Image<Bgr, Single>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_64F:
    //                        tDepth = typeof(Double);
    //                        return new Image<Bgr, Double>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    default:
    //                        throw new NotImplementedException(unsupportedDepth);
    //                }
    //            case 4:
    //                tColor = typeof(Bgra);
    //                switch (mi.depth)
    //                {
    //                    case IPL_DEPTH.IPL_DEPTH_8U:
    //                        tDepth = typeof(Byte);
    //                        return new Image<Bgra, Byte>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_16U:
    //                        tDepth = typeof(UInt16);
    //                        return new Image<Bgra, UInt16>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_16S:
    //                        tDepth = typeof(Int16);
    //                        return new Image<Bgra, Int16>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_32S:
    //                        tDepth = typeof(Int32);
    //                        return new Image<Bgra, Int32>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_32F:
    //                        tDepth = typeof(Single);
    //                        return new Image<Bgra, Single>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    case IPL_DEPTH.IPL_DEPTH_64F:
    //                        tDepth = typeof(Double);
    //                        return new Image<Bgra, Double>(mi.width, mi.height, mi.widthStep, mi.imageData);
    //                    default:
    //                        throw new NotImplementedException(unsupportedDepth);
    //                }
    //            default:
    //                throw new NotImplementedException(unsupportedChannels);
    //        }
    //    }

    //    /// <summary>
    //    /// 将Emgucv中的Image对象转换成IplImage指针；
    //    /// </summary>
    //    /// <typeparam name="TColor">Color type of this image (either Gray, Bgr, Bgra, Hsv, Hls, Lab, Luv, Xyz or Ycc)</typeparam>
    //    /// <typeparam name="TDepth">Depth of this image (either Byte, SByte, Single, double, UInt16, Int16 or Int32)</typeparam>
    //    /// <param name="image">Image对象</param>
    //    /// <returns>返回IplImage指针</returns>
    //    public static IntPtr EmgucvImageToIplImagePointer<TColor, TDepth>(Image<TColor, TDepth> image)
    //        where TColor : struct, IColor
    //        where TDepth : new()
    //    {
    //        return image.Ptr;
    //    }

    //    /// <summary>
    //    /// 将IplImage指针转换成位图对象；
    //    /// 对于不支持的像素格式，可以先使用cvCvtColor函数转换成支持的图像指针
    //    /// </summary>
    //    /// <param name="ptr">IplImage指针</param>
    //    /// <returns>返回位图对象</returns>
    //    public static Bitmap IplImagePointerToBitmap(IntPtr ptr)
    //    {
    //        MIplImage mi = IplImagePointerToMIplImage(ptr);
    //        PixelFormat pixelFormat;    //像素格式
    //        string unsupportedDepth = "不支持的像素位深度IPL_DEPTH";
    //        string unsupportedChannels = "不支持的通道数（仅支持1，2，4通道）";
    //        switch (mi.nChannels)
    //        {
    //            case 1:
    //                switch (mi.depth)
    //                {
    //                    case IPL_DEPTH.IPL_DEPTH_8U:
    //                        pixelFormat = PixelFormat.Format8bppIndexed;
    //                        break;
    //                    case IPL_DEPTH.IPL_DEPTH_16U:
    //                        pixelFormat = PixelFormat.Format16bppGrayScale;
    //                        break;
    //                    default:
    //                        throw new NotImplementedException(unsupportedDepth);
    //                }
    //                break;
    //            case 3:
    //                switch (mi.depth)
    //                {
    //                    case IPL_DEPTH.IPL_DEPTH_8U:
    //                        pixelFormat = PixelFormat.Format24bppRgb;
    //                        break;
    //                    case IPL_DEPTH.IPL_DEPTH_16U:
    //                        pixelFormat = PixelFormat.Format48bppRgb;
    //                        break;
    //                    default:
    //                        throw new NotImplementedException(unsupportedDepth);
    //                }
    //                break;
    //            case 4:
    //                switch (mi.depth)
    //                {
    //                    case IPL_DEPTH.IPL_DEPTH_8U:
    //                        pixelFormat = PixelFormat.Format32bppArgb;
    //                        break;
    //                    case IPL_DEPTH.IPL_DEPTH_16U:
    //                        pixelFormat = PixelFormat.Format64bppArgb;
    //                        break;
    //                    default:
    //                        throw new NotImplementedException(unsupportedDepth);
    //                }
    //                break;
    //            default:
    //                throw new NotImplementedException(unsupportedChannels);

    //        }
    //        Bitmap bitmap = new Bitmap(mi.width, mi.height, mi.widthStep, pixelFormat, mi.imageData);
    //        //对于灰度图像，还要修改调色板
    //        if (pixelFormat == PixelFormat.Format8bppIndexed)
    //            SetColorPaletteOfGrayscaleBitmap(bitmap);
    //        return bitmap;
    //    }

    //    /// <summary>
    //    /// 将位图转换成IplImage指针
    //    /// </summary>
    //    /// <param name="bitmap">位图对象</param>
    //    /// <returns>返回IplImage指针</returns>
    //    public static IntPtr BitmapToIplImagePointer(Bitmap bitmap)
    //    {
    //        IImage iimage = null;
    //        switch (bitmap.PixelFormat)
    //        {
    //            case PixelFormat.Format8bppIndexed:
    //                iimage = new Image<Gray, Byte>(bitmap);
    //                break;
    //            case PixelFormat.Format16bppGrayScale:
    //                iimage = new Image<Gray, UInt16>(bitmap);
    //                break;
    //            case PixelFormat.Format24bppRgb:
    //                iimage = new Image<Bgr, Byte>(bitmap);
    //                break;
    //            case PixelFormat.Format32bppArgb:
    //                iimage = new Image<Bgra, Byte>(bitmap);
    //                break;
    //            case PixelFormat.Format48bppRgb:
    //                iimage = new Image<Bgr, UInt16>(bitmap);
    //                break;
    //            case PixelFormat.Format64bppArgb:
    //                iimage = new Image<Bgra, UInt16>(bitmap);
    //                break;
    //            default:
    //                Image<Bgra, Byte> tmp1 = new Image<Bgra, Byte>(bitmap.Size);
    //                Byte[,,] data = tmp1.Data;
    //                for (int i = 0; i < bitmap.Width; i++)
    //                {
    //                    for (int j = 0; j < bitmap.Height; j++)
    //                    {
    //                        Color color = bitmap.GetPixel(i, j);
    //                        data[j, i, 0] = color.B;
    //                        data[j, i, 1] = color.G;
    //                        data[j, i, 2] = color.R;
    //                        data[j, i, 3] = color.A;
    //                    }
    //                }
    //                iimage = tmp1;
    //                break;
    //        }
    //        return iimage.Ptr;
    //    }

    //    /// <summary>
    //    /// 设置256级灰度位图的调色板
    //    /// </summary>
    //    /// <param name="bitmap"></param>
    //    public static void SetColorPaletteOfGrayscaleBitmap(Bitmap bitmap)
    //    {
    //        PixelFormat pixelFormat = bitmap.PixelFormat;
    //        if (pixelFormat == PixelFormat.Format8bppIndexed)
    //        {
    //            ColorPalette palette = bitmap.Palette;
    //            for (int i = 0; i < palette.Entries.Length; i++)
    //                palette.Entries[i] = Color.FromArgb(255, i, i, i);
    //            bitmap.Palette = palette;
    //        }
    //    }
    //}
}
