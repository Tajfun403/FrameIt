using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FrameIt.General;

internal interface ISelectable
{
    bool IsSelected
    {
        get; set;
    }

    bool IsSelectable
    {
        get; set;
    }
}
