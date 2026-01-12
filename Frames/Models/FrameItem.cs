using CommunityToolkit.Mvvm.ComponentModel;
using FrameIt.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrameIt.Models;

public partial class FrameItem : ObservableObject
{
    public int Id { get; set; }

    public string PairingCode { get; set; }

    public FrameConfig Config { get; set; } = new();

    // =====================
    // IMAGE PATH (PERSISTED)
    // =====================
    private string _imagePath;
    public string ImagePath
    {
        get => _imagePath;
        set
        {
            SetProperty(ref _imagePath, value);
            OnPropertyChanged(nameof(ImageBitmap));
        }
    }

    // =====================
    // IMAGE (UI ONLY)
    // =====================
    [JsonIgnore]
    public ImageSource ImageBitmap
    {
        get
        {
            if (string.IsNullOrWhiteSpace(ImagePath))
                return null;

            if (!File.Exists(ImagePath))
                return null;

            try
            {
                return new BitmapImage(new Uri(
                    Path.GetFullPath(ImagePath),
                    UriKind.Absolute));
            }
            catch
            {
                return null;
            }
        }
    }
}