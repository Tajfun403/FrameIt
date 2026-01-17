using CommunityToolkit.Mvvm.ComponentModel;
using FrameIt.General;
using FrameIt.Shows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FrameIt.Shows;

namespace FrameIt.Models;

public partial class FrameItem : ObservableObject
{
    public int Id { get; set; }

    public string PairingCode { get; set; }

    public FrameConfig Config { get; set; } = new();

    public ObservableCollection<PhotoShow> PhotoShows { get; set; }
    = new ObservableCollection<PhotoShow>();

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

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}