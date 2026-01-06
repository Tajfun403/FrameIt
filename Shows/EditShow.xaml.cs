using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameIt.Shows;

/// <summary>
/// Interaction logic for EditShow.xaml
/// </summary>
public partial class EditShow : Page, INotifyPropertyChanged
{
    public PhotoShow ShowContext
    {
        get; set
        {
            field = value;
            this.DataContext = value;
        }
    }
    public EditShow(PhotoShow show)
    {
        InitializeComponent();
        ShowContext = show;
    }

    public EditShow() : this(new PhotoShow()
    {
        DisplayName = "New PhotoShow",
    })
    {
    }

    public bool IsRenaming
    {
        get; set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotRenaming));
        }
    }

    public bool IsNotRenaming
    {
        get => !IsRenaming;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void RenameButton_Click(object sender, RoutedEventArgs e)
    {
        IsRenaming = true;
    }

    private void FinishRenaming_Click(object sender, RoutedEventArgs e)
    {
        IsRenaming = false;
    }

    private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        IsRenaming = true;
    }

    public RelayCommand FinishRenameCommand => new(() =>
    {
        IsRenaming = false;
        NameTextBox.Focus();
    });
}
