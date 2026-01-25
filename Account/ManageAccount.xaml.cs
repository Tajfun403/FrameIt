using System.Windows;
using System.Windows.Controls;
using FrameIt.General;
using Microsoft.Win32;

namespace FrameIt.Account;

public partial class ManageAccount : Page
{
    public UserAccount? CurrUser => AccountManager.Instance.CurrAccount;

    public ManageAccount()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void SaveAndNotify()
    {
        AccountManager.Instance.SaveUsers();
        AccountManager.Instance.CurrAccount = CurrUser;
    }

    private void PasswordInput_Changed(object sender, RoutedEventArgs e)
    {
        ConfirmPassBtn.IsEnabled = !string.IsNullOrWhiteSpace(PassInput.Password) &&
                                   !string.IsNullOrWhiteSpace(ConfirmPassInput.Password);
    }

    private void ChangePicture_Click(object sender, RoutedEventArgs e)
    {
        if (CurrUser == null) return;
        OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Images|*.png;*.jpg;*.jpeg" };
        if (openFileDialog.ShowDialog() == true)
        {
            CurrUser.AvatarPath = openFileDialog.FileName;
            SaveAndNotify();
            PopUpManager.ShowSuccess("Profile picture updated successfully!");
        }
    }

    private void ToggleEditName_Click(object sender, RoutedEventArgs e)
    {
        if (DisplayNameArea.Visibility == Visibility.Visible)
        {
            DisplayNameArea.Visibility = Visibility.Collapsed;
            EditNameArea.Visibility = Visibility.Visible;
            NameInput.Focus();
        }
        else
        {
            if (string.IsNullOrWhiteSpace(NameInput.Text)) return;
            EditNameArea.Visibility = Visibility.Collapsed;
            DisplayNameArea.Visibility = Visibility.Visible;
            SaveAndNotify();
            PopUpManager.ShowSuccess("Name updated successfully!");
        }
    }

    private void CancelEditName_Click(object sender, RoutedEventArgs e)
    {
        EditNameArea.Visibility = Visibility.Collapsed;
        DisplayNameArea.Visibility = Visibility.Visible;
    }

    private async void ToggleEditEmail_Click(object sender, RoutedEventArgs e)
    {
        if (EmailBtn.Visibility == Visibility.Visible)
        {
            EmailBtn.Visibility = Visibility.Collapsed;
            EditEmailArea.Visibility = Visibility.Visible;
            EmailInput.Focus();
        }
        else
        {
            if (string.IsNullOrWhiteSpace(EmailInput.Text) || !EmailInput.Text.Contains("@"))
            {
                PopUpManager.ShowError("Please enter a valid email.");
                return;
            }

            bool confirm = await PopUpManager.ShowYesNoDialog(
                "Confirm Email Change",
                $"Are you sure you want to change your email to '{EmailInput.Text}'?",
                true
            );

            if (!confirm) return;

            EditEmailArea.Visibility = Visibility.Collapsed;
            EmailBtn.Visibility = Visibility.Visible;
            CurrUser.Email = EmailInput.Text;
            SaveAndNotify();
            PopUpManager.ShowSuccess("Email updated successfully!");
        }
    }

    private void CancelEditEmail_Click(object sender, RoutedEventArgs e)
    {
        EditEmailArea.Visibility = Visibility.Collapsed;
        EmailBtn.Visibility = Visibility.Visible;
    }

    private async void ToggleEditPass_Click(object sender, RoutedEventArgs e)
    {
        if (PassBtn.Visibility == Visibility.Visible)
        {
            PassBtn.Visibility = Visibility.Collapsed;
            EditPassArea.Visibility = Visibility.Visible;
            PassInput.Focus();
            ConfirmPassBtn.IsEnabled = false;
        }
        else
        {
            string newPass = PassInput.Password;
            string confirmPass = ConfirmPassInput.Password;

            if (newPass != confirmPass)
            {
                PopUpManager.ShowError("Passwords do not match!");
                return;
            }

            if (CurrUser != null && newPass == CurrUser.Password)
            {
                PopUpManager.ShowError("New password must be different from the current one!");
                return;
            }

            bool confirm = await PopUpManager.ShowYesNoDialog(
                "Confirm Password Change",
                "Are you sure you want to change your password?",
                true
            );

            if (!confirm) return;

            if (CurrUser != null)
            {
                CurrUser.Password = newPass;
                SaveAndNotify();
                EditPassArea.Visibility = Visibility.Collapsed;
                PassBtn.Visibility = Visibility.Visible;
                PassInput.Password = "";
                ConfirmPassInput.Password = "";
                PopUpManager.ShowSuccess("Password updated successfully!");
            }
        }
    }

    private void CancelEditPass_Click(object sender, RoutedEventArgs e)
    {
        PassInput.Password = "";
        ConfirmPassInput.Password = "";
        EditPassArea.Visibility = Visibility.Collapsed;
        PassBtn.Visibility = Visibility.Visible;
    }

    private async void Logout_Click(object sender, RoutedEventArgs e)
    {
        bool confirm = await PopUpManager.ShowYesNoDialog(
            "Confirm Log Out",
            "Are you sure you want to log out?",
            false
        );

        if (!confirm) return;

        AccountManager.Instance.Logout();
        PopUpManager.ShowMessage("Logged out successfully.");
    }
}