using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

public interface IFilterBase
{
    public BitmapImage ApplyFilter(BitmapImage source);

    protected static Image<Rgba32> ToImageSharp(BitmapSource source)
    {
        int width = source.PixelWidth;
        int height = source.PixelHeight;
        int stride = width * 4;
        byte[] pixels = new byte[height * stride];
        source.CopyPixels(pixels, stride, 0);

        var image = Image.LoadPixelData<Rgba32>(pixels, width, height);
        return image;
    }

    protected static BitmapImage ToBitmapImage(Image<Rgba32> image)
    {
        int width = image.Width;
        int height = image.Height;
        var wb = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgra32, null);

        byte[] pixels = new byte[width * height * 4];
        image.CopyPixelDataTo(pixels);

        wb.WritePixels(new System.Windows.Int32Rect(0, 0, width, height), pixels, width * 4, 0);

        var encoder = new BmpBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(wb));
        using var ms = new MemoryStream();
        encoder.Save(ms);
        ms.Position = 0;

        var result = new BitmapImage();
        result.BeginInit();
        result.CacheOption = BitmapCacheOption.OnLoad;
        result.StreamSource = ms;
        result.EndInit();
        result.Freeze();
        return result;
    }

}
