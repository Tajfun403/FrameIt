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
}
