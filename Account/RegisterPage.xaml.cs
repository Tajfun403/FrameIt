using System.Windows;
using System.Windows.Controls;
using FrameIt.General;
using FrameIt.UI;

namespace FrameIt.Account;

public partial class RegisterPage : Page
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Handles the registration process, including field validation and account creation.
    /// </summary>
    private void Register_Click(object sender, RoutedEventArgs e)
    {
        // 1. Check if any fields are empty
        if (string.IsNullOrWhiteSpace(RegName.Text) ||
            string.IsNullOrWhiteSpace(RegEmail.Text) ||
            string.IsNullOrWhiteSpace(RegPass.Password) ||
            string.IsNullOrWhiteSpace(RegPassConfirm.Password))
        {
            MessageBox.Show("Please fill in all fields.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // 2. Validate email format (must contain @ and .)
        if (!RegEmail.Text.Contains("@") || !RegEmail.Text.Contains("."))
        {
            MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // 3. Validate if passwords match
        if (RegPass.Password != RegPassConfirm.Password)
        {
            MessageBox.Show("Passwords do not match! Please re-enter your password.", "Password Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // 4. Check if the account already exists in the system
        if (AccountManager.Instance.DoesAccountExist(RegEmail.Text))
        {
            MessageBox.Show("An account with this email already exists!", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            return;
        }

        // 5. Create new user object
        var newUser = new UserAccount
        {
            Name = RegName.Text,
            Email = RegEmail.Text,
            Password = RegPass.Password,
            AvatarPath = "Images/GrayLiara.jpg", // Default avatar
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