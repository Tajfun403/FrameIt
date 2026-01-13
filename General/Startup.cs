using FrameIt.UI;
using FrameIt.Account;
using System;
using System.Linq;

namespace FrameIt.General;

internal static class Startup
{
    internal static void NavigateToStartPage()
    {
        var args = Environment.GetCommandLineArgs();

        if (args.Contains("-StartToHome", StringComparer.OrdinalIgnoreCase))
        {
            NavigationManager.Navigate(new Home(), false, true);
            return;
        }

        if (AccountManager.Instance.IsLoggedIn)
        {
            NavigationManager.GoToHome();
        }
        else
        {
            NavigationManager.StatusVM.ShowNavigation = false;
            NavigationManager.Navigate(new LoginHome(), false, true);
        }
    }
}