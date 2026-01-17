using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FrameIt.General;

class NavigationBarStatusVM : ObservableObject
{
    public bool CanGoBack
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public bool ShowNavigation
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    
    // added account visibility - Julia
    public Visibility IsAccountBarVisible
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = Visibility.Visible;
}
