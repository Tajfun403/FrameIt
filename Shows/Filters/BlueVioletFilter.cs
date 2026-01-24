using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Dithering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

internal class BlueVioletFilter : IFilterBase
{
    public BitmapImage ApplyFilter(BitmapImage source)
    {
        using var image = IFilterBase.ToImageSharp(source);
        IDither dither = KnownDitherings.FloydSteinberg;
        Color lowerColor = new(new Rgba32(67, 149, 212));
        Color upperColor = new(new Rgba32(137, 84, 182));
        image.Mutate(x => x.BinaryDither(dither, lowerColor, upperColor));
        return IFilterBase.ToBitmapImage(image);
    }
}
