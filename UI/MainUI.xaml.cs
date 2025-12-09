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
        AccountManager.OnUserChanged += RefreshAccount;
        RefreshAccount();
        NavigationManager.Init(this);
    }

    public void RefreshAccount()
    {
        AccountName.DataContext = AccountManager.CurrAccount;
        AvatarEllipse.DataContext = AccountManager.CurrAccount;
    }
}
