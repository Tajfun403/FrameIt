using FrameIt.Frames.AddFrame;
using FrameIt.General;
using FrameIt.Models;
using FrameIt.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace FrameIt.Frames
{
    public partial class FramesMain : Page, INotifyPropertyChanged
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

        private void FramesMain_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadFrames();
        }

        // =====================
        // NAVIGATION
        // =====================

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

        // =====================
        // DELETE MODE
        // =====================
        public bool IsInDeleteMode
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DeleteButtonText));
            }
        }

        public bool ItemsSelectable
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged();
            }
        }

        public string DeleteButtonText =>
            IsInDeleteMode ? "Cancel Delete" : "Delete Frame";

        private void DeleteFrameClick(object sender, RoutedEventArgs e)
        {
            if (IsInDeleteMode)
                ExitDeleteMode();
            else
                EnterDeleteMode();
        }

        private void EnterDeleteMode()
        {
            IsInDeleteMode = true;
            ItemsSelectable = true;
        }

        private void ExitDeleteMode()
        {
            IsInDeleteMode = false;
            ItemsSelectable = false;

            // TODO: wyczyścić zaznaczenia
        }

        private void ConfirmDeleteFrames(object sender, RoutedEventArgs e)
        {
            var selectedFrames = Frames
                .Where(f => f.IsSelected)
                .ToList();

            foreach (var frame in selectedFrames)
                Frames.Remove(frame);

            var count = selectedFrames.Count;
            PopUpManager.ShowSuccess($"{count} {(count == 1 ? "frame" : "frames")} deleted.");

            ExitDeleteMode();
        }

        // =====================
        // INotifyPropertyChanged
        // =====================

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
