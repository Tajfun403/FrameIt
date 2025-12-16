using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FrameIt.General;

/// <summary>
/// This is a base template for the WPF's NotifyPropertyChanged framework.<br/>
/// You can inherit from this class directly if your class is a POCO,<br/>
/// or copy paste it to your class if it's not
/// </summary>
internal class PropChangedBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
