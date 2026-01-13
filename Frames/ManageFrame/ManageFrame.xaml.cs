using FrameIt.General;
using FrameIt.Models;
using FrameIt.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FrameIt.Frames;

namespace FrameIt.Frames.ManageFrame
{
    public partial class ManageFrame : Page
    {
        public ManageFrame(int frameId)
        {
            InitializeComponent();

            var frame = FramesManager.LoadFrames()
                                     .First(f => f.Id == frameId);

            DataContext = new ManageFrameViewModel(frame);
        }

        private void DeleteFrame_Click(object sender, RoutedEventArgs e)
        {
            // UPDATE THIS
            if (MessageBox.Show(
                "Are you sure you want to delete this frame?",
                "Confirm delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ((ManageFrameViewModel)DataContext)
                    .DeleteFrameCommand.Execute(null);

                NavigationManager.GoToHome();
            }
        }
    }
}
