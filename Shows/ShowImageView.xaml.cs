using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameIt.Shows;

/// <summary>
/// Interaction logic for ShowImageView.xaml
/// </summary>
public partial class ShowImageView : Page, INotifyPropertyChanged
{
    public ShowImage ImageContext
    {
        get; set
        {
            field = value;
            this.DataContext = value;
            OnPropertyChanged();
            CurrImage = ImageContext.ImageBitmap;
            SetupFilters();
        }
    }

    public BitmapImage CurrImage
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Filters.FilterViewModel> FiltersList
    {
        get;
    } = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetupFilters()
    {
        FiltersList.Clear();
        var filters = Filters.FiltersManager.GetAllFilterViewModels(ImageContext.ImageBitmap);
        foreach (var item in filters)
        {
            FiltersList.Add(item);
        }
    }

    public ShowImageView()
    {
        InitializeComponent();
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
