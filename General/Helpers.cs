using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameIt.General;

internal static class Helpers
{
    public static BitmapImage BitmapToWPF(this Bitmap src)
    {
        // https://stackoverflow.com/a/34590774
        MemoryStream ms = new();
        ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        BitmapImage image = new();
        image.BeginInit();
        ms.Seek(0, SeekOrigin.Begin);
        image.StreamSource = ms;
        image.EndInit();
        return image;
    }
}
