using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters;

internal static class FiltersManager
{
    public static List<IFilterBase> GetAllFilters()
    {
        return [
            new NothingFilter(),
            new GrayscaleFilter(),
            new VioletifierFilter(),
            new SepiaFilter(),
            new BlackWhiteFilter(),
            new LomographFilter(),
            // new BlueVioletFilter(),
            new OilPaint(),
        ];
    }

    public static ObservableCollection<FilterViewModel> GetAllFilterViewModels(BitmapImage originalImage)
    {
        var filters = GetAllFilters();
        var collection = new ObservableCollection<FilterViewModel>();
        foreach (var filter in filters)
        {
            collection.Add(new FilterViewModel()
            {
                Filter = filter,
                OriginalImage = originalImage
            });
        }
        return collection;
    }
}
