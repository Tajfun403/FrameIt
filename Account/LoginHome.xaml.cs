using FrameIt.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

namespace FrameIt.Account;

/// <summary>
/// Interaction logic for LoginHome.xaml
/// </summary>
public partial class LoginHome : Page, INotifyPropertyChanged
{
    public LoginHome()
    {
        InitializeComponent();
        DataContext = this;
    }

    public bool IsACorrectEmail(string email)
    {
        return email.Contains("@") && email.Contains(".") && email[^1..^2] is not "." and not "@";
    }

    public string Email
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsContinueEnabled));
        }
    }

    public bool IsContinueEnabled => IsACorrectEmail(Email);

    public bool RememberMe { get; set; }
    public string Password { get; set; }

    public bool IsLoginOrRegisterVisible
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    } = true;
    public bool IsLoginVisible
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
        }
    } = false;


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void LogInOrRegister_Continue_Click(object sender, RoutedEventArgs e)
    {
        if (DoesAccountExist(Email))
        {
            IsLoginVisible = true;
            IsLoginOrRegisterVisible = false;
        }
        else
        {
            // TODO NAVIGATE TO REGISTER PAGE
        }
    }

    private bool DoesAccountExist(string email)
    {
        // TODO IMPLEMENT ACTUAL LOGIC!
        return true;
    }

    private void LogIn_Continue_Click(object sender, RoutedEventArgs e)
    {
        // TODO CHECK PASSWORD, IMPLEMENTS ERRORS IF IT GOES WRONG!
        // TODO TRANSFORM TO THE MAIN PAGE
        // TODO LOGIN
        NavigationManager.GoToHome();
    }
}
