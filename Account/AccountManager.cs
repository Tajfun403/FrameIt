using FrameIt.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace FrameIt.Account;

class AccountManager : PropChangedBase
{
    /// <summary>
    /// Currently logged-in user account.<br/>
    /// Data-bound in the navigation UI, so do *not* null it out!
    /// </summary>
    public UserAccount CurrAccount { get; 
        set
        {
            field = value;
            OnUserChanged?.Invoke();
        }
    }

    /// <summary>
    /// Data-bound to the view of user state in the navigation bar.
    /// </summary>
    public bool IsLoggedIn
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsAccountBarVisible));

        }
    } = false;

    public Visibility IsAccountBarVisible => IsLoggedIn ? Visibility.Visible : Visibility.Collapsed;

    public static AccountManager Instance { get; } = new AccountManager();

    public void LoadDefaultAccount()
    {
        CurrAccount = new()
        {
            Name = "Sarivian",
            Email = "Sarivian@gmail.com",
            AvatarPath = "Images/Liara.jpg"
        };
        IsLoggedIn = true;
    }

    /// <summary>
    /// Event triggered when the currently logged in account changes
    /// </summary>
    public event Action OnUserChanged = delegate { };

    public AccountManager()
    {
        LoadDefaultAccount();
    }

    /// <summary>
    /// Attempt to use stored login info to login immediately.
    /// </summary>
    /// <returns>Whether the auto login succeeded</returns>
    public bool TryAutoLogin()
    {
        return true;
        // TODO Implement this!
        throw new NotImplementedException();
    }

    public bool TryLogin(string Username, string Password)
    {
        return true;
    }

    public bool Register(string Username, string Password)
    {
        throw new NotImplementedException();
    }

}
