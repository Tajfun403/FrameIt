using System;
using System.Collections.Generic;
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
/// Interaction logic for ShowsMain.xaml
/// </summary>
public partial class ShowsMain : Page
{
    private PhotoShow defaultShow = new();
    public ShowsMain()
    {
        InitializeComponent();
        defaultShow.PhotosList.Add(new ShowImage()
        {
            ImagePath = "Images/Liara.jpg",
            DisplayName = "Sample Photo 1"
        });
        defaultShow.PhotosList.Add(new ShowImage()
        {
            ImagePath = "Images/GrayLiara.jpg",
            DisplayName = "Sample Photo 2"
        });
        ShowsList.DataContext = defaultShow;
    }
}
