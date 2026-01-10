using FrameIt.Models;
using FrameIt.Services;
using System.Linq;
using System.Windows.Controls;

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
    }
}
