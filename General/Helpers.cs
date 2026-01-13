using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
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

    public static T? FindVisualChild<T>(DependencyObject parent)
    where T : DependencyObject
    {
        // https://stackoverflow.com/a/19097670
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is T typedChild)
                return typedChild;

            var result = FindVisualChild<T>(child);
            if (result != null)
                return result;
        }
        return null;
    }

}
