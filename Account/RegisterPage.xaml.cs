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

    private void Register_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(RegName.Text) || string.IsNullOrWhiteSpace(RegPass.Password))
        {
            MessageBox.Show("Please fill in all fields.");
            return;
        }

        if (AccountManager.Instance.DoesAccountExist(RegEmail.Text))
        {
            MessageBox.Show("Email already exists!");
            return;
        }

        var newUser = new UserAccount
        {
            Name = RegName.Text,
            Email = RegEmail.Text,
            Password = RegPass.Password,
            AvatarPath = "Images/GrayLiara.jpg",
            RememberMe = false
        };

        AccountManager.Instance.Register(newUser);

        NavigationManager.GoToHome();
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        NavigationManager.GoBack();
    }
}