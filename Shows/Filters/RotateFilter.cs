using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

internal class RotateFilter : IFilterBase
{
    public Rotation Rot { get; set; }

    public RotateFilter(Rotation rot)
    {
        Rot = rot;
    }

    public RotateFilter() : this(Rotation.Rotate90)
    {
    }

    public BitmapImage ApplyFilter(BitmapImage source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        double angle = Rot switch
        {
            Rotation.Rotate90 => 90,
            Rotation.Rotate180 => 180,
            Rotation.Rotate270 => 270,
            Rotation.Rotate0 => 0,
            _ => 0
        };

        var transformed = new TransformedBitmap();
        transformed.BeginInit();
        transformed.Source = source;
        transformed.Transform = new RotateTransform(angle);
        transformed.EndInit();

        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(transformed));

        using var stream = new MemoryStream();
        encoder.Save(stream);
        stream.Position = 0;

        var result = new BitmapImage();
        result.BeginInit();
        result.CacheOption = BitmapCacheOption.OnLoad;
        result.StreamSource = stream;
        result.EndInit();
        result.Freeze();

        return result;
    }
}
