using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

public class FilterViewModel
{
    public IFilterBase Filter { get; set; }
    public BitmapImage OriginalImage { get; set; }
    public ImageSource PreviewImage
    {
        get
        {
            if (Filter != null && OriginalImage != null)
            {
                return Filter.ApplyFilter(OriginalImage);
            }
            return OriginalImage;
        }
    }

}
