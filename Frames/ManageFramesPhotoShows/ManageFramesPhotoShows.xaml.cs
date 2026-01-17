using FrameIt.General;
using FrameIt.Services;
using FrameIt.Shows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FrameIt.Frames.ManageFramesPhotoShows
{
    public partial class ManageFramesPhotoShows : Page
    {
        private readonly int _frameId;

        public ObservableCollection<PhotoShow> Shows { get; }
        public string HeaderText { get; }

        public ManageFramesPhotoShows(int frameId)
        {
            InitializeComponent();

            _frameId = frameId;

            var frame = FramesManager.LoadFrames()
                                     .First(f => f.Id == frameId);

            HeaderText = $"{frame.Config.Name} – Manage PhotoShows";

            Shows = frame.PhotoShows;

            DataContext = this;
        }

        private void AddPhotoShowsClick(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(
                new AddPhotoShows(frameId: _frameId),
                CanMoveBack: true,
                ShowNavigationPanel: true
            );
        }

        private void DeletePhotoShowsClick(object sender, RoutedEventArgs e)
        {
            // TODO: delete mode later
        }
    }
}
