using FrameIt.Account;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace FrameIt.General;

/// <summary>
/// Interaction logic for NavigationHeader.xaml
/// </summary>
public partial class NavigationHeader : UserControl
{
    public NavigationHeader()
    {
        InitializeComponent();
        AccountManager.Instance.OnUserChanged += RefreshAccount;
        RefreshAccount();
    }

    public void Account_Click(object sender, MouseButtonEventArgs e)
    {
        NavigationManager.Navigate(new ManageAccount(), true);
    }

    public void RefreshAccount()
    {
        AccountName.DataContext = AccountManager.Instance.CurrAccount;
        AvatarEllipse.DataContext = AccountManager.Instance.CurrAccount;
        AccountBar.DataContext = AccountManager.Instance;
    }

    public void GoBack_Click(object sender, MouseButtonEventArgs e)
    {
        NavigationManager.GoBack();
    }
}
