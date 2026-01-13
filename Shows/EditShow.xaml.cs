using CommunityToolkit.Mvvm.Input;
using FrameIt.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
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
/// Interaction logic for EditShow.xaml
/// </summary>
public partial class EditShow : Page, INotifyPropertyChanged
{
    public PhotoShow ShowContext
    {
        get; set
        {
            field?.PropertyChanged -= CollectionChanged_Handler;
            field = value;
            this.DataContext = value;
            value.PropertyChanged += CollectionChanged_Handler;
        }
    }

    private void CollectionChanged_Handler(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(PhotosCountString));
    }

    ~EditShow()
    {
        // Not guaranteed to work?
        ShowContext?.PropertyChanged -= CollectionChanged_Handler;
    }


    public EditShow(PhotoShow show)
    {
        InitializeComponent();
        ShowContext = show;
        CommandDeletePhotos = new(DoDeletePhotos, () => CanDeleteSelectedPhotos);
    }

    public EditShow() : this(new PhotoShow()
    {
        DisplayName = "PlaceHolder PhotoShow",
    })
    {
    }

    public bool IsRenaming
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotRenaming));
        }
    }

    public bool IsNotRenaming
    {
        get => !IsRenaming;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void RenameButton_Click(object sender, RoutedEventArgs e)
    {
        IsRenaming = true;
    }

    private void FinishRenaming_Click(object sender, RoutedEventArgs e)
    {
        IsRenaming = false;
    }

    private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        IsRenaming = true;
    }

    public RelayCommand FinishRenameCommand => new(() =>
    {
        IsRenaming = false;
        NameTextBox.Focus();
    });

    private void DropOverlay_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }
    }

    public bool IsUserDragging
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    private void DropOverlay_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop)!;
            Debug.WriteLine($"Dropping: {files.Length} files...");
            AddFiles(files);
        }
        IsUserDragging = false;
    }

    protected async Task AddFiles(string[] files)
    {
        foreach (string file in files)
        {
            ShowContext.AddImageByPath(file);
        }
        PopUpManager.ShowMessage($"{files.Length} {(files.Length == 1 ? "image" : "images")} added to the show.");
    }

    private void DropOverlay_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }
    }

    private void Page_DragEnter(object sender, DragEventArgs e)
    {
        IsUserDragging = true;
    }

    private void Page_DragLeave(object sender, DragEventArgs e)
    {
        IsUserDragging = false;
    }

    public string PhotosCountString => $"{ShowContext.PhotosList.Count} Photos";

    private void BrowseButton_Click(object sender, RoutedEventArgs e)
    {
        Microsoft.Win32.OpenFileDialog openFileDialog = new()
        {
            Multiselect = true,
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.webp|All Files|*.*"
        };
        bool? result = openFileDialog.ShowDialog();
        if (result == true)
        {
            AddFiles(openFileDialog.FileNames);
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        ShowContext?.PropertyChanged += CollectionChanged_Handler;
        OnPropertyChanged(nameof(ItemsMargin));
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        ShowContext?.PropertyChanged -= CollectionChanged_Handler;
    }

    public Thickness ItemsMargin
    {
        get => GetCorrectMargin();
    }

    protected Thickness GetCorrectMargin()
    {
        const double additionMargin = 30;
        // double currPageWidth = this.ActualWidth - additionMargin;

        var scroller = Helpers.FindVisualChild<ScrollViewer>(PhotosListControl);
        double currPageWidth = (scroller?.ViewportWidth - 22) ?? ActualWidth - additionMargin;

        double itemWidth = 140; // TODO GET THIS CORRECTLY!
        int fullItems = (int)(currPageWidth / itemWidth);
        double leftOverSpace = currPageWidth - (fullItems * itemWidth);
        double remainingMargin = leftOverSpace / (fullItems + 1);
        Debug.WriteLine($"CurrPageWidth: {ActualWidth}; ScrollerWidth: {scroller?.ViewportWidth ?? 0}; Extra space: {remainingMargin}");
        return new(remainingMargin / 2, 0, remainingMargin / 2, 0);
        
    }

    private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ItemsMargin));
    }

    private void PhotoClicked(ShowImage img)
    {
        var viewer = new ShowImageView()
        {
            ImageContext = img
        };
        NavigationManager.Navigate(viewer, true);
    }

    public RelayCommand<ShowImage> PhotoClickedCommand => new(PhotoClicked);

    #region DeletingItems

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

    public string DelPhotosButtonText => IsInDeleteMode ? "Cancel Delete" : "Delete images";

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
        foreach (var item in ShowContext.PhotosList)
        {
            item.IsSelected = false;
        }
        // TODO Selectable is on items -- clear it after exiting delete mode
        // TODO Call this when lost focus!
    }

    private void DoDeletePhotos()
    {
        var imgsToDel = ShowContext.PhotosList.Where(x => x.IsSelected).ToList();
        foreach (ShowImage img in imgsToDel)
        {
            ShowContext.PhotosList.Remove(img);
        }
        ExitDeleteMode();
        PopUpManager.ShowMessage($"{imgsToDel.Count} {(imgsToDel.Count == 1 ? "photo" : "photos")} deleted.");
        OnPropertyChanged(nameof(PhotosCountString));
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
        get => ShowContext.PhotosList.Any(x => x.IsSelected);
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

    #endregion DeletingItems
}
