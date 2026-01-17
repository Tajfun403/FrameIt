using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

internal class VioletifierFilter : IFilterBase
{
    protected System.Windows.Media.Color OverlayColor = System.Windows.Media.Color.FromRgb(250, 100, 150);
    public BitmapImage ApplyFilter(BitmapImage source)
    {
        // It is reversed, for whatever the reason
        var realColor = OverlayColor;
        realColor.R = OverlayColor.B;
        realColor.B = OverlayColor.R;

        using var image = IFilterBase.ToImageSharp(source);
        var mutate = (IImageProcessingContext ctx) =>
        {
            ctx.Saturate(0f);
        };
        image.Mutate(mutate);
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var pixelRow = accessor.GetRowSpan(y);
                for (int x = 0; x < pixelRow.Length; x++)
                {
                    var pixel = pixelRow[x];
                    pixel.R = OverlayByte(pixel.R, OverlayColor.R);
                    pixel.G = OverlayByte(pixel.G, OverlayColor.G);
                    pixel.B = OverlayByte(pixel.B, OverlayColor.B);
                    pixelRow[x] = pixel;
                }
            }
        });
        return IFilterBase.ToBitmapImage(image);
    }

    /// <summary>
    /// PS-like overlay blend mode for bytes
    /// </summary>
    /// <param name="original"></param>
    /// <param name="overlay"></param>
    /// <returns></returns>
    protected static byte OverlayByte(byte original, byte overlay)
    {
        if (original < 128)
            return (byte)(2 * original * overlay / 255);
        else
            return (byte)(255 - 2 * (255 - original) * (255 - overlay) / 255);

    }
}
