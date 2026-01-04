using FrameIt.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameIt.General;

internal static class Startup
{
    internal static void NavigateToStartPage()
    {
        var args = Environment.GetCommandLineArgs();
        if (args.Contains("-StartToHome", StringComparer.OrdinalIgnoreCase))
        {
            NavigationManager.Navigate(new Home(), false, true);
        }
        else if (args.Contains("-StartToLogin", StringComparer.OrdinalIgnoreCase))
        {
            NavigationManager.Navigate(new Account.LoginHome(), false, false);
        }
        else
        {
            NavigationManager.Navigate(new Account.LoginHome(), false, true);
        }
    }
}
