using FrameIt.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FrameIt.Shows;

class PhotoShow : PropChangedBase, ISelectable
{
    public ObservableCollection<ShowImage> PhotosList { get; } = [];
    public bool IsSelected { get; set; }
    public bool IsSelectable { get; set; }
}
