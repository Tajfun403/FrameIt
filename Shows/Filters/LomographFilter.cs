using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

internal class LomographFilter : IFilterBase
{
    public BitmapImage ApplyFilter(BitmapImage source)
    {
        using var image = IFilterBase.ToImageSharp(source);
        image.Mutate(x => x.Lomograph());
        return IFilterBase.ToBitmapImage(image);
    }
}
