using CommunityToolkit.Mvvm.Input;
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
        ShowContext?.PropertyChanged -= CollectionChanged_Handler;
    }

    public EditShow(PhotoShow show)
    {
        InitializeComponent();
        ShowContext = show;
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
}
