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
    public Guid Id { get; set; } = Guid.NewGuid();


    public PhotoShow()
    {
        PhotosList.CollectionChanged += (s, e) =>
        {
            if (PhotosList.Count > 0)
            {
                var firstImg = PhotosList.First();
                try
                {
                    PhotosList.First().PropertyChanged -= PhotoShow_PropertyChanged;
                }
                catch { }
                PhotosList.First().PropertyChanged += PhotoShow_PropertyChanged;
            }
        };
    }

    private void PhotoShow_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ImageBitmap));
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
    }

    // TODO Make this refresh!
    public ImageSource ImageBitmap
    {
        get
        {
            if (PhotosList.Count == 0)
                return ShowImage.PlaceholderImage;
            return PhotosList[0].GetThumbnailSync();
        }
    }

    public async void AddImageByPath(string path)
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
