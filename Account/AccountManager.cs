using System;
using System.Collections.Generic;
using System.Text;

namespace FrameIt.Account;

class AccountManager
{
    public static UserAccount CurrAccount { get; 
        set
        {
            field = value;
            OnUserChanged?.Invoke();
        }
    }

    public static void LoadDefaultAccount()
    {
        CurrAccount = new()
        {
            Name = "Sarivian",
            Email = "Sarivian@gmail.com",
            AvatarPath = "Images/Liara.jpg"
        }; 
    }

    /// <summary>
    /// Event triggered when the currently logged in account changes
    /// </summary>
    public static event Action OnUserChanged = delegate { };

    static AccountManager()
    {
        LoadDefaultAccount();
    }

    /// <summary>
    /// Attempt to use stored login info to login immediately.
    /// </summary>
    /// <returns>Whether the auto login succeeded</returns>
    public static bool TryAutoLogin()
    {
        return true;
        // TODO Implement this!
        throw new NotImplementedException();
    }

    public static bool TryLogin(string Username, string Password)
    {
        return true;
    }

    public static bool Register(string Username, string Password)
    {
        throw new NotImplementedException();
    }
}
