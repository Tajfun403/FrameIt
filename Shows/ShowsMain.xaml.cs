using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Navigation;
using FrameIt.General;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace FrameIt.Shows;

/// <summary>
/// Interaction logic for ShowsMain.xaml
/// </summary>
public partial class ShowsMain : Page, INotifyPropertyChanged
{
    private PhotoShow defaultShow = new();

    public ObservableCollection<PhotoShow> ShowsCollection { get; private set; } = [];

    public ShowsMain()
    {
        InitializeComponent();
        defaultShow.PhotosList.Add(new ShowImage()
        {
            ImagePath = "Images/Liara.jpg",
            DisplayName = "Sample Photo 1"
        });
        defaultShow.PhotosList.Add(new ShowImage()
        {
            ImagePath = "Images/GrayLiara.jpg",
            DisplayName = "Sample Photo 2"
        });
        defaultShow.DisplayName = "Sample PhotoShow";

        ShowsCollection.Add(defaultShow);

        // defaultShow.DisplayName = "Sample PhotoShow";
        // ShowsList.DataContext = defaultShow;

        // TODO Set the real source here!
        // ShowsList.DataContext = ShowsManager.Instance.Shows;
        // this.DataContext = ShowsManager.Instance.Shows;

        // TODO Add filtering shows from a given frame
        // TODO add delete form

        // This NEEDS to be set in ctor!
        // Setting this in initializer is buggy
        CommandDeletePhotos = new(DoDeletePhotos, () => CanDeleteSelectedPhotos);
    }

    public bool IsInDeleteMode
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    } = false;

    public bool ItemsSelectable
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    } = false;

    private void DeletePhotos_Click(object sender, RoutedEventArgs e)
    {
        if (IsInDeleteMode) 
        {
            ExitDeleteMode();
        }
        else
        {
            EnterDeleteMode();
        }
    }

    public string DelPhotosButtonText => IsInDeleteMode ? "Cancel Delete" : "Delete PhotoShows";

    public void OnPageLeft(Page left)
    {
        ExitDeleteMode();
    }

    public void EnterDeleteMode()
    {
        IsInDeleteMode = true;
        ItemsSelectable = true;
        OnPropertyChanged(nameof(DelPhotosButtonText));
        NavigationManager.RegisterOnPageLeft(OnPageLeft);
    }

    public void ExitDeleteMode()
    {
        IsInDeleteMode = false;
        ItemsSelectable = false;
        OnPropertyChanged(nameof(DelPhotosButtonText));
        NavigationManager.UnregisterOnPageLeft();
        foreach (var item in ShowsCollection)
        {
            item.IsSelected = false;
        }
        // TODO Selectable is on items -- clear it after exiting delete mode
        // TODO Call this when lost focus!
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void DoDeletePhotos()
    {
        var showsToDel = ShowsCollection.Where(x => x.IsSelected).ToList();
        foreach (PhotoShow show in showsToDel)
        {
            ShowsCollection.Remove(show);
        }
        ExitDeleteMode();
        // IsInDeleteMode = false;
    }

    private void SelectionChanged()
    {
        OnPropertyChanged(nameof(CanDeleteSelectedPhotos));
        CommandDeletePhotos.NotifyCanExecuteChanged();
    }

    public RelayCommand SelectionChangedCommand => new(SelectionChanged);

    public bool CanDeleteSelectedPhotos
    {
        get => ShowsCollection.Any(x => x.IsSelected);
    }

    public RelayCommand<PhotoShow> ShowClickedCommand => new(OnShowClicked);

    public void OnShowClicked(PhotoShow show)
    {
        if (IsInDeleteMode)
        {
            show.IsSelected = !show.IsSelected;
            SelectionChanged();
        }
        else
        {
            NavigationManager.Navigate(new EditShow(show),
            true);
        }
    }


    // This NEEDS to be set in ctor!
    // Setting this in initializer is buggy
    public RelayCommand CommandDeletePhotos { get; init; }
    //public RelayCommand CommandDeletePhotos => new(DoDeletePhotos, () => CanDeleteSelectedPhotos);

}
