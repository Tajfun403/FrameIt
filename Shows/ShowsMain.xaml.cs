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

namespace FrameIt.Shows;

/// <summary>
/// Interaction logic for ShowsMain.xaml
/// </summary>
public partial class ShowsMain : Page, INotifyPropertyChanged
{
    private PhotoShow defaultShow = new();
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
        ShowsList.DataContext = defaultShow;

        // TODO Set the real source here!
        // ShowsList.DataContext = ShowsManager.Instance.Shows;
        // this.DataContext = ShowsManager.Instance.Shows;

        // TODO Add filtering shows from a given frame
        // TODO add delete form
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

    }

    private void SelectionChanged()
    {
        CommandDeletePhotos.NotifyCanExecuteChanged();
    }

    public bool CanDeleteSelectedPhotos
    {
        get => true; // TODO IMPLEMENT
    }

    public RelayCommand CommandDeletePhotos => new(DoDeletePhotos, () => CanDeleteSelectedPhotos);
}
