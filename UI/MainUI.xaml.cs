using FrameIt.Account;
using FrameIt.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameIt.UI;

/// <summary>
/// Interaction logic for MainUI.xaml
/// </summary>
public partial class MainUI : Page
{
    public MainUI()
    {
        InitializeComponent();
        NavigationManager.Init(this);
        // Moved auto-login logic from Startup/LoginHome to MainUI constructor. - Julia
        if (AccountManager.Instance.IsLoggedIn)
        {
            NavigationManager.GoToHome();
        }
        else
        {
            NavigationManager.StatusVM.ShowNavigation = false;
            NavigationManager.Navigate(new LoginHome(), false, false, false);
        }
    }
}