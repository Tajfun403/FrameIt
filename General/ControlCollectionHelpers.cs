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

    public static List<T> GetSelectedItems<T>(this IEnumerable<T> nodesList) where T : ISelectable
    {
        List<T> selectedItems = [];
        foreach(var node in nodesList)
        {
            if (node.IsSelected && node is T tNode)
            {
                selectedItems.Add(tNode);
            }
        }
        return selectedItems;
    }
}
