using FrameIt.Frames.AddFrame;
using FrameIt.General;
using FrameIt.Models;
using FrameIt.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FrameIt.Frames
{
    public partial class FramesMain : Page
    {
        public ObservableCollection<FrameItem> Frames { get; }

        public FramesMain()
        {
            InitializeComponent();

            Frames = new ObservableCollection<FrameItem>(
                FramesManager.LoadFrames()
            );

            DataContext = this;

            Loaded += FramesMain_Loaded;
        }

        private void AddFrameClick(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(
                new AddFrameCode(),
                CanMoveBack: true,
                ShowNavigationPanel: false
            );
        }

        private void ManageFrameClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
                button.DataContext is FrameItem frame)
            {
                NavigationManager.Navigate(
                    new ManageFrame.ManageFrame(frame.Id),
                    CanMoveBack: true,
                    ShowNavigationPanel: true
                );
            }
        }

        private void ReloadFrames()
        {
            Frames.Clear();

            foreach (var frame in FramesManager.LoadFrames())
                Frames.Add(frame);
        }

        private void FramesMain_Loaded(object sender, RoutedEventArgs e)
{
        ReloadFrames();
}

    }
}
