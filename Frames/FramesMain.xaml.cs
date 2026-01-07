using FrameIt.Frames.AddFrame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameIt.Frames
{
    public partial class FramesMain : Page
    {
        public ObservableCollection<FrameItem> Frames => _framesManager.Frames;

        private readonly FramesManager _framesManager;

        public FramesMain()
        {
            this.InitializeComponent();

            _framesManager = new FramesManager();
            DataContext = this;
        }

        private void AddFrame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddFrameCode());
        }
    }
}