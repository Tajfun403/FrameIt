using FrameIt.General;
using FrameIt.Services;
using FrameIt.Shows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FrameIt.Frames.ManageFramesPhotoShows
{
    public partial class AddPhotoShows : Page
    {
        private readonly int _frameId;

        public ObservableCollection<PhotoShow> Shows { get; }

        public AddPhotoShows(int frameId)
        {
            InitializeComponent();

            _frameId = frameId;

            var frame = FramesManager.LoadFrames()
                                     .First(f => f.Id == frameId);

            Shows = ShowsManager.Instance.Shows;

            foreach (var show in Shows)
            {
                show.IsSelectable = true;
                show.IsSelected = frame.PhotoShows.Contains(show);
            }

            DataContext = this;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var frame = FramesManager.LoadFrames()
                                     .First(f => f.Id == _frameId);

            frame.PhotoShows.Clear();

            foreach (var show in Shows.Where(s => s.IsSelected))
            {
                frame.PhotoShows.Add(show);
            }

            FramesManager.SaveFrame(frame);

            NavigationManager.GoBack();
        }
    }
}
