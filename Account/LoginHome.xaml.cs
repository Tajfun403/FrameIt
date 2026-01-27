using CommunityToolkit.Mvvm.Input;
using FrameIt.General;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FrameIt.Account;

public partial class LoginHome : Page, INotifyPropertyChanged
{
    public LoginHome()
    {
        LogInOrRegisterCommand = new RelayCommand(LogInOrRegister_Continue_Func, () => IsContinueEnabled);

        InitializeComponent();
        DataContext = this;
    }

    public string Email { get => field; set { field = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsContinueEnabled)); LogInOrRegisterCommand.NotifyCanExecuteChanged(); } }
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }

    public bool IsLoginOrRegisterVisible { get => field; set { field = value; OnPropertyChanged(); } } = true;
    public bool IsLoginVisible { get => field; set { field = value; OnPropertyChanged(); } } = false;

    public bool IsContinueEnabled => !string.IsNullOrEmpty(Email) && Email.Contains("@") && Email.Contains(".");
    public RelayCommand LogInOrRegisterCommand { get; init; }

    /// <summary>
    /// Checks if the account exists to decide whether to show the password field or redirect to registration.
    /// </summary>
    private void LogInOrRegister_Continue_Func()
    {
        if (AccountManager.Instance.DoesAccountExist(Email))
        {
            IsLoginVisible = true;
            IsLoginOrRegisterVisible = false;
        }
        else
        {
            PopUpManager.ShowMessage("Email not found. Redirecting to registration...");
            NavigationManager.Navigate(new RegisterPage(Email), true, false, false);
        }
    }

    private void BackToEmail_Click(object sender, RoutedEventArgs e)
    {
        IsLoginVisible = false;
        IsLoginOrRegisterVisible = true;
        LoginPassBox.Password = string.Empty;
        Password = string.Empty;
    }

    /// <summary>
    /// Manually updates the Password property since PasswordBox.Password is not bindable for security reasons.
    /// </summary>
    private void LoginPassBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = ((PasswordBox)sender).Password;
    }

    /// <summary>
    /// Finalizes the login process by verifying credentials and handling the navigation to Home.
    /// </summary>
    private void LogIn_Continue_Click(object sender, RoutedEventArgs e)
    {
        if (AccountManager.Instance.TryLogin(Email, Password, RememberMe))
        {
            NavigationManager.GoToHome();
        }
        else
        {
            PopUpManager.ShowError("Invalid password. Please try again.");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string n = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}