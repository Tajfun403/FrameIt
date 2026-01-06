using FrameIt.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows;

public class ShowImage : INotifyPropertyChanged, ISelectable
{
    public string ImagePath
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ImageBitmap));
        }
    }

    public ImageSource ImageBitmap
    {
        get
        {
            if (string.IsNullOrEmpty(ImagePath))
                return null;
            if (!File.Exists(ImagePath))
                throw new FileNotFoundException($"Image of path {ImagePath} not found!");
            string fullPath = new FileInfo(ImagePath).FullName;
            try
            {
                return new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            }
            catch
            {
                return null;
            }
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
