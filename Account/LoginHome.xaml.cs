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

        this.Loaded += LoginHome_Loaded;
    }

    /// <summary>
    /// Handles automatic redirection to Home if a user session is already active (Remember Me).
    /// </summary>
    private void LoginHome_Loaded(object sender, RoutedEventArgs e)
    {
        if (AccountManager.Instance.IsLoggedIn)
        {
            this.Loaded -= LoginHome_Loaded;
            NavigationManager.GoToHome();
        }
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
            MessageBox.Show("Email not found. Redirecting to registration.");
            NavigationManager.Navigate(new RegisterPage(), true, false, false);
        }
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
            this.Loaded -= LoginHome_Loaded;
            NavigationManager.GoToHome();
        }
        else
        {
            MessageBox.Show("Invalid password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string n = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}