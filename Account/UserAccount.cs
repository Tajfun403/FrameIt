using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.IO;
using System;
using System.Text.Json.Serialization;

namespace FrameIt.Account;

public class UserAccount : INotifyPropertyChanged
{

    public required string Name
    {
        get;
        set
        {
            field = value; 
            OnPropertyChanged();
        }
    }

    public required string Email
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public required string Password
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public string AvatarPath
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AvatarImage));
        }
    } = "Images/UserAvatarDefault.png";

    public bool RememberMe
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [JsonIgnore]
    public BitmapImage? AvatarImage
    {
        get
        {
            if (string.IsNullOrEmpty(AvatarPath) || !File.Exists(AvatarPath)) return null;
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Path.GetFullPath(AvatarPath), UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }
            catch { return null; }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string p = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}