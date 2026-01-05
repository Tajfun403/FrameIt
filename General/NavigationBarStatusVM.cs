using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

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
}
