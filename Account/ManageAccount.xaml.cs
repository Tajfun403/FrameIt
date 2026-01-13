using System.Windows;
using System.Windows.Controls;
using FrameIt.General;
using Microsoft.Win32;

namespace FrameIt.Account;

public partial class ManageAccount : Page
{
    // Shortcut to the currently logged-in account
    public UserAccount? CurrUser => AccountManager.Instance.CurrAccount;

    public ManageAccount()
    {
        InitializeComponent();
        DataContext = this;
    }

    /// <summary>
    /// Saves current user data to the JSON database and triggers a UI refresh 
    /// for the navigation bar (avatar and display name).
    /// </summary>
    private void SaveAndNotify()
    {
        AccountManager.Instance.SaveUsers();
        // Re-assigning triggers the property change notification for the UI
        AccountManager.Instance.CurrAccount = CurrUser;
    }

    /// <summary>
    /// Opens a file dialog to select a new profile picture and updates the user's avatar path.
    /// </summary>
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

    /// <summary>
    /// Toggles between displaying the user's name and the input field to edit it.
    /// Saves changes when switching back to display mode.
    /// </summary>
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

    /// <summary>
    /// Cancels the name editing process and restores the original display view.
    /// </summary>
    private void CancelEditName_Click(object sender, RoutedEventArgs e)
    {
        EditNameArea.Visibility = Visibility.Collapsed;
        DisplayNameArea.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Toggles the email editing interface. Validates the email format before saving.
    /// </summary>
    private void ToggleEditEmail_Click(object sender, RoutedEventArgs e)
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
            EditEmailArea.Visibility = Visibility.Collapsed;
            EmailBtn.Visibility = Visibility.Visible;
            SaveAndNotify();
            PopUpManager.ShowSuccess("Email updated successfully!");
        }
    }

    /// <summary>
    /// Cancels the email editing process and returns to the standard display view.
    /// </summary>
    private void CancelEditEmail_Click(object sender, RoutedEventArgs e)
    {
        EditEmailArea.Visibility = Visibility.Collapsed;
        EmailBtn.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Toggles the password editing interface and handles password update logic.
    /// </summary>
    private void ToggleEditPass_Click(object sender, RoutedEventArgs e)
    {
        if (PassBtn.Visibility == Visibility.Visible)
        {
            PassBtn.Visibility = Visibility.Collapsed;
            EditPassArea.Visibility = Visibility.Visible;
            PassInput.Focus();
        }
        else
        {
            if (string.IsNullOrWhiteSpace(PassInput.Password)) return;

            if (CurrUser != null)
            {
                CurrUser.Password = PassInput.Password;
                SaveAndNotify();
                EditPassArea.Visibility = Visibility.Collapsed;
                PassBtn.Visibility = Visibility.Visible;
                PassInput.Password = "";
                PopUpManager.ShowSuccess("Password updated successfully!");
            }
        }
    }

    /// <summary>
    /// Cancels the password change process and clears the password input field.
    /// </summary>
    private void CancelEditPass_Click(object sender, RoutedEventArgs e)
    {
        PassInput.Password = "";
        EditPassArea.Visibility = Visibility.Collapsed;
        PassBtn.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Prompts the user for confirmation and logs out of the current session if confirmed.
    /// </summary>
    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        AccountManager.Instance.Logout();
        PopUpManager.ShowMessage("Logged out successfully.");

    }
}