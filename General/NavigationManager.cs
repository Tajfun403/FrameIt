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

    public static void Navigate(Page page, bool CanMoveBack, bool ShowNavigationPanel = true, bool ShowAccountBar = true)
    {
        // TODO Do show account bar!
        Debug.WriteLine($"Navigating to {page}");
        StatusVM.CanGoBack = CanMoveBack;
        StatusVM.ShowNavigation = ShowNavigationPanel;

        //Update Account Bar visibility based on the navigation target - Julia
        StatusVM.IsAccountBarVisible = ShowAccountBar ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

        onPageLeft?.Invoke((Page)MainFrame.Content!);
        onPageLeft = null;
        GC.Collect();
        MainFrame.Navigate(page);
        //Added ShowAccountBar to the stack entry - Julia
        NavigationStack.Push(new(page, CanMoveBack, ShowNavigationPanel, ShowAccountBar));
    }

    public static void GoBack()
    {
        PrintNavigationStack();
        if (!CanGoBack())
            return;
        NavigationStack.Pop();
        var lastPage = NavigationStack.Peek();
        MainFrame.Navigate(lastPage.Page);
        StatusVM.CanGoBack = lastPage.CanMoveBack;
        StatusVM.ShowNavigation = lastPage.ShowNavigationPanel;
        // Restoring Account Bar visibility state from the previous page - Julia
        StatusVM.IsAccountBarVisible = lastPage.ShowAccountBar ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        // MainFrame.GoBack();
    }

    public static void PrintNavigationStack()
    {
        Debug.WriteLine("Navigation Stack:");
        foreach (var entry in NavigationStack)
        {
            Debug.WriteLine($" - {entry}");
        }
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

    private static Action<Page>? onPageLeft;

    /// <summary>
    /// Execute an <see cref="Action{Page}"/> when the current page is left.<br/>
    /// Remember to Unregister with <see cref="UnregisterOnPageLeft"/> when no longer needed.
    /// </summary>
    /// <param name="onPageLeft"></param>
    public static void RegisterOnPageLeft(Action<Page> onPageLeft)
    {
        NavigationManager.onPageLeft = onPageLeft;
    }

    /// <summary>
    /// Unregisters the current handler for the page left event, if any.
    /// </summary>
    public static void UnregisterOnPageLeft()
    {
        NavigationManager.onPageLeft = null;
    }

    //Added ShowAccountBar property 
    public record struct NavigationEntry(Page Page, bool CanMoveBack, bool ShowNavigationPanel, bool ShowAccountBar);

    public static NavigationBarStatusVM StatusVM = new();
}