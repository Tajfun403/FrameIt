using CommunityToolkit.Mvvm.ComponentModel;
using FrameIt.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace FrameIt.Account;

/// <summary>
/// Manages user sessions, registration, and local data persistence using JSON.
/// </summary>
partial class AccountManager : ObservableObject
{
    private List<UserAccount> _users = new();
    private const string DbPath = "Data/users.json";

    // Data-bound property for the currently active user 
    public UserAccount? CurrAccount
    {
        get;
        set
        {
            field = value;
            OnUserChanged?.Invoke();
            OnPropertyChanged();
        }
    }

    // Controls whether the user is authenticated
    public bool IsLoggedIn
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsAccountBarVisible));
        }
    }

    // Helper property for UI Binding to handle Account Bar visibility
    public Visibility IsAccountBarVisible => IsLoggedIn ? Visibility.Visible : Visibility.Collapsed;

    // Singleton instance for global access
    public static AccountManager Instance { get; } = new AccountManager();

    public event Action OnUserChanged = delegate { };

    private AccountManager()
    {
        LoadUsers();
    }

    /// <summary>
    /// Loads users from local JSON file and handles the "Remember Me" auto-login.
    /// </summary>
    private void LoadUsers()
    {
        if (!File.Exists(DbPath)) return;
        try
        {
            string json = File.ReadAllText(DbPath);
            _users = JsonSerializer.Deserialize<List<UserAccount>>(json) ?? new();

            // Auto-login logic based on the 'RememberMe' flag
            var remembered = _users.FirstOrDefault(u => u.RememberMe);
            if (remembered != null)
            {
                // Set directly to the property to trigger proper state
                CurrAccount = remembered;
                IsLoggedIn = true;
            }
        }
        catch { _users = new(); }
    }

    /// <summary>
    /// Synchronizes the current user list to the local JSON database.
    /// </summary>
    public void SaveUsers()
    {
        if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(DbPath, JsonSerializer.Serialize(_users, options));
    }

    /// <summary>
    /// Signs out the user, clears the "Remember Me" flag, and redirects to Login.
    /// </summary>
    public void Logout()
    {
        if (CurrAccount != null) CurrAccount.RememberMe = false;
        IsLoggedIn = false;
        CurrAccount = null;
        SaveUsers();

        // Return to login screen and reset navigation UI states
        NavigationManager.StatusVM.ShowNavigation = false;
        NavigationManager.Navigate(new LoginHome(), false, false, false);
    }

    public bool DoesAccountExist(string email)
        => _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Validates credentials and initializes the session.
    /// </summary>
    public bool TryLogin(string email, string password, bool rememberMe)
    {
        var user = _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == password);
        if (user != null)
        {
            // Reset RememberMe for all other users to ensure only one active session
            _users.ForEach(u => u.RememberMe = false);

            user.RememberMe = rememberMe;
            CurrAccount = user;
            IsLoggedIn = true;
            SaveUsers();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds a new user to the database and logs them in immediately.
    /// </summary>
    public void Register(UserAccount newUser)
    {
        _users.Add(newUser);
        CurrAccount = newUser;
        IsLoggedIn = true;
        SaveUsers();
    }
}