using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameIt.Account;

class UserAccount : INotifyPropertyChanged
{
    /// <summary>
    /// User's name
    /// </summary>
    public string Name
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    }
    /// <summary>
    /// User's email
    /// </summary>
    public string Email
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Path to the user's profile image. <br/>
    /// We assume that it is never uploaded anywhere and stays local.
    /// </summary>
    public string AvatarPath
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AvatarImage));
        }
    }

    public BitmapImage AvatarImage
    {
        get
        {
            if (string.IsNullOrEmpty(AvatarPath))
                return null;
            if (!File.Exists(AvatarPath))
                throw new FileNotFoundException($"Avatar of path {AvatarPath} not found!");
            string fullPath = new FileInfo(AvatarPath).FullName;
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
