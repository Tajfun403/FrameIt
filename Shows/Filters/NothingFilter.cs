using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

internal class NothingFilter : IFilterBase
{
    public BitmapImage ApplyFilter(BitmapImage source)
    {
        return source;
    }
}
