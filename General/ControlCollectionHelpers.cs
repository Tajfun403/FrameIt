using FrameIt.UI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FrameIt.General;

internal static class ControlCollectionHelpers
{
    public static void ToggleSelectable(this IEnumerable<ISelectable> nodesList, bool isVisible)
    {
        foreach(var node in nodesList)
        {
            node.IsSelectable = isVisible;
        }
    }
}
