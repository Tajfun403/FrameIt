using FrameIt.UI;
using FrameIt.Account;
using System;
using System.Linq;
using FrameIt.Shows;

namespace FrameIt.General;

internal static class Startup
{
    internal static void NavigateToStartPage()
    {
        var args = Environment.GetCommandLineArgs();
        ShowsManager.Instance.GenerateDefaultShows();

        if (args.Contains("-StartToHome", StringComparer.OrdinalIgnoreCase))
        {
            AccountManager.Instance.TryLogin("sarivian@gmail.com", "TaliIsMyGirl", false);
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