using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.IO;
using System;
using System.Text.Json.Serialization;

namespace FrameIt.Account;

public class UserAccount : INotifyPropertyChanged
{
    private string _name;
    private string _email;
    private string _password;
    private string _avatarPath = "Images/GrayLiara.jpg";
    private bool _rememberMe;

    public required string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public required string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    public required string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public string AvatarPath
    {
        get => _avatarPath;
        set
        {
            _avatarPath = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AvatarImage));
        }
    }

    public bool RememberMe
    {
        get => _rememberMe;
        set { _rememberMe = value; OnPropertyChanged(); }
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