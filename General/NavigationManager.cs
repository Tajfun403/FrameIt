using FrameIt.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace FrameIt.General;

class NavigationManager
{
    public static MainUI MainWindow { get; private set; }

    public static Frame MainFrame => MainWindow.MainContentFrame;

    public static Stack<NavigationEntry> NavigationStack { get; } = [];

    public static void Init(MainUI mainWindow)
    {
        MainWindow = mainWindow;
        GoToHome();
    }

    public static void Navigate(Page page, bool CanMoveBack, bool ShowNavigationPanel = true)
    {
        MainFrame.Navigate(page);
        NavigationStack.Push(new(page, CanMoveBack, ShowNavigationPanel));
    }

    public static void GoBack()
    {
        if (!CanGoBack())
            return;
        NavigationStack.Pop();
        MainFrame.GoBack();
    }

    public static bool CanGoBack()
    {
        if (NavigationStack.Count == 0)
            return false;
        return NavigationStack.Peek().CanMoveBack && MainFrame.CanGoBack;
    }

    public static void GoToHome()
    {
        while (MainFrame.CanGoBack)
        {
            MainFrame.RemoveBackEntry();
        }
        NavigationStack.Clear();
        Navigate(new UI.Home(), false, true);
    }

    public record struct NavigationEntry(Page Page, bool CanMoveBack, bool ShowNavigationPanel);
}
