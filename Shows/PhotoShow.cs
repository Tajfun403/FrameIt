using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FrameIt.Shows;

class PhotoShow : INotifyPropertyChanged
{
    public ObservableCollection<ShowImage> PhotosList { get; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;
}
