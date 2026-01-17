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
using FrameIt.Shows.Filters;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FrameIt.Shows;

public class ShowImage : ObservableObject, ISelectable
{
    public ShowImage()
    {
        FiltersStack.CollectionChanged += (s, e) => OnFiltersStackChanged();
    }

    public string ImagePath
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
            RefreshImages();
        }
    }

    public static BitmapImage PlaceholderImage = new BitmapImage(new Uri("Images/GrayLiara.jpg", uriKind: UriKind.RelativeOrAbsolute));

    public async Task RefreshImages()
    {
        ImagesLoaded = false;
        Thumbnail = PlaceholderImage;
        await LoadMainImage();
        await RefreshThumbnail();
        SourceThumbnail = Thumbnail;
        SourceImageBitmap = ImageBitmap;
        ImagesLoaded = true;
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
        if (ImagesLoaded)
        {
            return Thumbnail;
        }
        // else fetch it live
        var file = ShellFile.FromFilePath(ImagePath);
        var bitmap = file?.Thumbnail.ExtraLargeBitmap.BitmapToWPF() ?? PlaceholderImage;
        return ApplyFilters(bitmap);
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

    protected bool ImagesLoaded { get; private set; } = false;

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

    protected BitmapImage SourceThumbnail;

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

    protected BitmapImage SourceImageBitmap;

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

    public ObservableCollection<IFilterBase> FiltersStack { get; } = [];

    protected BitmapImage ApplyFilters(BitmapImage source)
    {
        BitmapImage current = source;
        foreach (var filter in FiltersStack)
        {
            current = filter.ApplyFilter(current);
        }
        return current;
    }

    protected void OnFiltersStackChanged()
    {
        ImageBitmap = ApplyFilters(SourceImageBitmap);
        Thumbnail = ApplyFilters(SourceThumbnail);
    }
}
