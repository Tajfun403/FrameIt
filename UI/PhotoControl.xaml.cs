using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FrameIt.UI;

/// <summary>
/// Interaction logic for PhotoControl.xaml
/// </summary>
public partial class PhotoControl : UserControl, INotifyPropertyChanged
{
    public PhotoControl()
    {
        InitializeComponent();
        // never set datacontext to self, as it breaks bindings
        // https://stackoverflow.com/a/76192782
    }

    public bool IsSelectable 
    {
        get => (bool)GetValue(IsSelectableProperty);
        set => SetValue(IsSelectableProperty, value);
    }
    public ImageSource Image
    {
        get => (ImageSource)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }
    public bool IsSelected 
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    public Visibility SelectionVisibility
    {
        get => IsSelectable ? Visibility.Visible : Visibility.Collapsed;
    }

    public bool ShouldShowName
    {
        get => (bool)GetValue(ShouldShowNameProperty);
        set => SetValue(ShouldShowNameProperty, value);
    }

    public string DisplayName
    {
        get => (string)GetValue(DisplayNameProperty);
        set => SetValue(DisplayNameProperty, value);
    }

    public static readonly DependencyProperty DisplayNameProperty =
        DependencyProperty.Register(nameof(DisplayName), typeof(string), typeof(PhotoControl));

    protected const float ImageOnlyWidth = 140;
    protected const float WithNameWidth = 300;

    public float DesiredWidth => ShouldShowName ? WithNameWidth : ImageOnlyWidth;

    public static readonly DependencyProperty ShouldShowNameProperty =
        DependencyProperty.Register(nameof(ShouldShowName), typeof(bool), typeof(PhotoControl),
            new PropertyMetadata(false, OnShouldShowNameChanged));

    private static void OnShouldShowNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (PhotoControl)d;

        ctrl.OnPropertyChanged(nameof(DesiredWidth));
    }

    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register(nameof(Image), typeof(ImageSource), typeof(PhotoControl));

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(PhotoControl));

    public static readonly DependencyProperty IsSelectableProperty =
       DependencyProperty.Register(nameof(IsSelectable), typeof(bool), typeof(PhotoControl));

    public static readonly DependencyProperty SecondaryNameProperty =
        DependencyProperty.Register(nameof(SecondaryName), typeof(string), typeof(PhotoControl));

    public string SecondaryName
    {
        get => (string)GetValue(SecondaryNameProperty);
        set
        {
            SetValue(SecondaryNameProperty, value);
            OnPropertyChanged(nameof(ShouldDisplaySecondaryName));
        }
    }

    public bool ShouldDisplaySecondaryName
    {
        get => !string.IsNullOrEmpty(SecondaryName);
    }

    public ICommand ClickCommand
    {
        get => (ICommand)GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }

    public static readonly DependencyProperty ClickCommandProperty =
        DependencyProperty.Register(nameof(ClickCommand), typeof(ICommand), typeof(PhotoControl));

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
