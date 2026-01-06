using CommunityToolkit.Mvvm.ComponentModel;
using FrameIt.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows;

public class PhotoShow : ObservableObject, ISelectable
{
    public ObservableCollection<ShowImage> PhotosList { get; } = [];
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
    }

    public ImageSource ImageBitmap
    {
        get
        {
            if (PhotosList.Count == 0)
                return new BitmapImage(new Uri("Images/GrayLiara.jpg", UriKind.Absolute));
            return PhotosList[0].ImageBitmap;
        }
    }

    public void AddImageByPath(string path)
    {
        ShowImage img = new()
        {
            ImagePath = path,
            DisplayName = System.IO.Path.GetFileNameWithoutExtension(path)
        };
        PhotosList.Add(img);
        OnPropertyChanged(nameof(ImageBitmap));
    }
}
