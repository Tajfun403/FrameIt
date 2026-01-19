using FrameIt.UI;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FrameIt.General;

internal class PopUpManager
{
    public static void ShowSuccess(string message)
    {
       mainUI.SuccessSnackbar.MessageQueue?.Enqueue(message);
    }

    public static void ShowMessage(string message)
    {
        mainUI.MainSnackbar.MessageQueue?.Enqueue(message);
    }

    private static MainUI mainUI;

    public static void Init(MainUI mainUI)
    {
        PopUpManager.mainUI = mainUI;
    }

    public static void ShowError(string message)
    {
        mainUI.ErrorSnackbar.MessageQueue?.Enqueue(message);
    }

    public static void ReturnDialogResult(bool result)
    {
        DialogHost.CloseDialogCommand.Execute(result, null);
    }

    public static async Task<bool> ShowYesNoDialog(string title, string mainText, bool isPositive = false)
    {
        YesNoDialog dialog = new()
        {
            Title = title,
            MainText = mainText,
            IsYesPositive = isPositive
        };
        var result = await DialogHost.Show(dialog, "RootDialog");
        return result is true;
    }
}
