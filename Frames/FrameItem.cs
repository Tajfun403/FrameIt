using CommunityToolkit.Mvvm.ComponentModel;
using FrameIt.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrameIt.Frames;

public class FrameItem : ObservableObject
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }

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
}
