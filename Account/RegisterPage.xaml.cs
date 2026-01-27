using System.Windows;
using System.Windows.Controls;
using FrameIt.General;
using FrameIt.UI;

namespace FrameIt.Account;

public partial class RegisterPage : Page
{
    public RegisterPage() : this(string.Empty) { }

    public RegisterPage(string initialEmail)
    {
        InitializeComponent();

        RegEmail.Text = initialEmail;

        RegName.TextChanged += (s, e) => UpdateRegisterButtonState();
        RegEmail.TextChanged += (s, e) => UpdateRegisterButtonState();
        RegPass.PasswordChanged += (s, e) => UpdateRegisterButtonState();
        RegPassConfirm.PasswordChanged += (s, e) => UpdateRegisterButtonState();

        UpdateRegisterButtonState();
    }

    private void UpdateRegisterButtonState()
    {
        RegisterButton.IsEnabled = !string.IsNullOrWhiteSpace(RegName.Text) &&
                                   !string.IsNullOrWhiteSpace(RegEmail.Text) &&
                                   !string.IsNullOrWhiteSpace(RegPass.Password) &&
                                   !string.IsNullOrWhiteSpace(RegPassConfirm.Password);
    }

    /// <summary>
    /// Handles the registration process, including field validation and account creation.
    /// </summary>
    private void Register_Click(object sender, RoutedEventArgs e)
    {
        // 2. Validate email format (must contain @ and .)
        if (!RegEmail.Text.Contains("@") || !RegEmail.Text.Contains("."))
        {
            PopUpManager.ShowError("Please enter a valid email address.");
            return;
        }

        // 3. Validate if passwords match
        if (RegPass.Password != RegPassConfirm.Password)
        {
            PopUpManager.ShowError("Passwords do not match! Please re-enter your password.");
            return;
        }

        // 4. Check if the account already exists in the system
        if (AccountManager.Instance.DoesAccountExist(RegEmail.Text))
        {
            PopUpManager.ShowError("An account with this email already exists!");
            return;
        }

        // 5. Create new user object
        var newUser = new UserAccount
        {
            Name = RegName.Text,
            Email = RegEmail.Text,
            Password = RegPass.Password,
            AvatarPath = "Images/UserAvatarDefault.png", // Default avatar
            RememberMe = false
        };

        // 6. Register and navigate to Home
        AccountManager.Instance.Register(newUser);
        NavigationManager.GoToHome();
    }

    /// <summary>
    /// Navigates back to the previous page (usually LoginHome).
    /// </summary>
    private void Back_Click(object sender, RoutedEventArgs e)
    {
        NavigationManager.GoBack();
    }
}