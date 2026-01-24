using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

internal class OilPaint : IFilterBase
{
    public BitmapImage ApplyFilter(BitmapImage source)
    {
        int imgWidth = source.PixelWidth;
        int imgHeight = source.PixelHeight;
        int avg = (imgWidth + imgHeight) / 2;
        const double brushSizeFactor = .05;
        const int levels = 25;

        using var image = IFilterBase.ToImageSharp(source);

        // --- DOWNSCALING ---
        const double targetSide = 500;
        var scaleFactor = targetSide / avg;
        int downWidth = (int)(imgWidth * scaleFactor);
        int downHeight = (int)(imgHeight * scaleFactor);

        image.Mutate(ctx =>
        {
            ctx.Resize(downWidth, downHeight); // downscale first
            ctx.OilPaint(levels, (int)(avg * brushSizeFactor * scaleFactor)); // adjust brush size
            ctx.Resize(imgWidth, imgHeight); // optional: upscale back to original size
            ctx.GaussianSharpen(1.5f);
        });

        return IFilterBase.ToBitmapImage(image);
    }

}
