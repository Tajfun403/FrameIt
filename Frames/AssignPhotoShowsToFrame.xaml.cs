using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FrameIt.Frames
{
    public partial class AssignPhotoShowsToFrame : Page
    {
        public ObservableCollection<ShowMock> Shows { get; }
        public string FrameTitle { get; }

        public AssignPhotoShowsToFrame(int frameId)
        {
            InitializeComponent();

            FrameTitle = $"Assign photo shows to frame #{frameId}";

            // FAKE DANE – wszystkie pokazy na koncie
            Shows = new ObservableCollection<ShowMock>
            {
                new ShowMock { Name = "Vacation 2024", IsAssigned = true },
                new ShowMock { Name = "Family", IsAssigned = false },
                new ShowMock { Name = "Wedding", IsAssigned = true },
                new ShowMock { Name = "Nature", IsAssigned = false }
            };

            DataContext = this;
        }
    }
}
