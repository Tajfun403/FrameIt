using FrameIt.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Shell;

namespace FrameIt.Shows;

public class ShowImage : INotifyPropertyChanged, ISelectable
{
    public string ImagePath
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
            RefreshImages();
        }
    }

    private static BitmapImage PlaceholderImage = new BitmapImage(new Uri("Images/GrayLiara.jpg", uriKind: UriKind.RelativeOrAbsolute));

    public async Task RefreshImages()
    {
        Thumbnail = PlaceholderImage;
        await LoadMainImage();
        await RefreshThumbnail();
    }

    private async Task RefreshThumbnail()
    {
        var file = ShellFile.FromFilePath(ImagePath);
        if (file.Thumbnail != null)
        {
            Thumbnail = file.Thumbnail.ExtraLargeBitmap.BitmapToWPF();
        }
        else
        {
            Thumbnail = PlaceholderImage;
        }
    }

    public ImageSource GetThumbnailSync()
    {
        var file = ShellFile.FromFilePath(ImagePath);
        return file?.Thumbnail.ExtraLargeBitmap.BitmapToWPF() ?? PlaceholderImage; 
    }

    private async Task LoadMainImage()
    {
        if (string.IsNullOrEmpty(ImagePath) || !File.Exists(ImagePath))
        {
            Thumbnail = PlaceholderImage;
            ImageBitmap = PlaceholderImage;
            return;
        }
        try
        {
            string fullPath = new FileInfo(ImagePath).FullName;
            ImageBitmap = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
        }
        catch
        {
            Thumbnail = PlaceholderImage;
            ImageBitmap = PlaceholderImage;
        }
    }

    /// <summary>
    /// Returns a small-res thumbmnail for previews.
    /// </summary>
    public BitmapImage Thumbnail
    {
        get; private set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    // TODO DO NOT LOAD ENTIRE PHOTOS!
    // Create thumbnails and store them in temp!
    public BitmapImage ImageBitmap
    {
        get; private set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public bool IsSelected
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public bool IsSelectable
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public string DisplayName
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    } = "PhotoShow";

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
