using CommunityToolkit.Mvvm.Input;
using FrameIt.General;
using FrameIt.Shows.Filters;
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
            // CurrImage = ImageContext.ImageBitmap;
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
        var filters = Filters.FiltersManager.GetAllFilterViewModels(ImageContext.SourceImageBitmap);
        foreach (var item in filters)
        {
            FiltersList.Add(item);
        }
        // Restore filter state
        foreach (var filter in ImageContext.FiltersStack)
        {
            if (filter is RotateFilter rotFilter)
            {
                CurrRot = rotFilter.Rot;
            }
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

    protected void OnFilterChanged(FilterViewModel filter)
    {
        ImageContext.FiltersStack.Clear();
        ImageContext.FiltersStack.Add(filter.Filter);
        ImageContext.FiltersStack.Add(new RotateFilter(CurrRot));
    }
    public RelayCommand<FilterViewModel> ChangeFilterCommand => new(OnFilterChanged);

    protected Rotation CurrRot;

    private void RotateButton_Click(object sender, RoutedEventArgs e)
    {
        CurrRot = CurrRot switch
        {
            Rotation.Rotate0 => Rotation.Rotate90,
            Rotation.Rotate90 => Rotation.Rotate180,
            Rotation.Rotate180 => Rotation.Rotate270,
            Rotation.Rotate270 => Rotation.Rotate0,
            _ => Rotation.Rotate0,
        };
        var collection = ImageContext.FiltersStack;
        bool found = false;
        // TODO do this correctly
        for (int i = collection.Count - 1; i >= 0; i--)
        {
            if (collection[i] is RotateFilter rotFilter)
            {
                rotFilter.Rot = CurrRot;
                ImageContext.RefreshFilters();
                found = true;
            }
        }
        if (!found)
        {
            ImageContext.FiltersStack.Add(new RotateFilter(CurrRot));
        }
        SetupFilters();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationManager.GoBack();
        var abovePage = NavigationManager.NavigationStack.Peek().Page as EditShow;
        if (abovePage == null)
        {
            PopUpManager.ShowError("Could not remove image from the show.");
            return;
        }
        abovePage!.ShowContext.PhotosList.Remove(ImageContext);
        PopUpManager.ShowSuccess("Image removed from the show.");
    }
}
