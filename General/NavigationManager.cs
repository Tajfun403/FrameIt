using FrameIt.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    }

    public static void Navigate(Page page, bool CanMoveBack, bool ShowNavigationPanel = true)
    {
        Debug.WriteLine($"Navigating to {page}");
        MainFrame.Navigate(page);
        StatusVM.CanGoBack = CanMoveBack;
        StatusVM.ShowNavigation = ShowNavigationPanel;
        NavigationStack.Push(new(page, CanMoveBack, ShowNavigationPanel));
    }

    public static void GoBack()
    {
        if (!CanGoBack())
            return;
        NavigationStack.Pop();
        MainFrame.Navigate( NavigationStack.Peek().Page );
        // MainFrame.GoBack();
    }

    public static bool CanGoBack()
    {
        if (NavigationStack.Count == 0)
            return false;
        // MainFrame.CanGoBack does not work
        return NavigationStack.Peek().CanMoveBack;
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

    public static NavigationBarStatusVM StatusVM = new();
}
