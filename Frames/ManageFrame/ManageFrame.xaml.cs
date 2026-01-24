using FrameIt.General;
using FrameIt.Models;
using FrameIt.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FrameIt.Frames;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

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
            // TODO
            /*
            if (PopUpManager.ShowYes(
                "Are you sure you want to delete this frame?",
                "Confirm delete",
                PopUpManagerButton.YesNo,
                PopUpManagerImage.Warning) == PopUpManagerResult.Yes)
            {
                ((ManageFrameViewModel)DataContext)
                    .DeleteFrameCommand.Execute(null);

                NavigationManager.GoToHome();
            }
            */
        }
    }

    public class WidgetsConfig : ObservableObject
    {
        private TimeWidget _time;
        private DateWidget _date;
        private WeatherWidget _weather;

        public TimeWidget Time
        {
            get => _time;
            set
            {
                if (!ReferenceEquals(_time, value))
                {
                    UnsubscribeFromWidget(_time);
                    _time = value;
                    SubscribeToWidget(_time);
                    OnPropertyChanged();
                }
            }
        }

        public DateWidget Date
        {
            get => _date;
            set
            {
                if (!ReferenceEquals(_date, value))
                {
                    UnsubscribeFromWidget(_date);
                    _date = value;
                    SubscribeToWidget(_date);
                    OnPropertyChanged();
                }
            }
        }

        public WeatherWidget Weather
        {
            get => _weather;
            set
            {
                if (!ReferenceEquals(_weather, value))
                {
                    UnsubscribeFromWidget(_weather);
                    _weather = value;
                    SubscribeToWidget(_weather);
                    OnPropertyChanged();
                }
            }
        }

        private void SubscribeToWidget(object? widget)
        {
            if (widget is INotifyPropertyChanged npc)
                npc.PropertyChanged += NestedWidget_PropertyChanged;
        }

        private void UnsubscribeFromWidget(object? widget)
        {
            if (widget is INotifyPropertyChanged npc)
                npc.PropertyChanged -= NestedWidget_PropertyChanged;
        }

        private void NestedWidget_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (ReferenceEquals(sender, _time))
                OnPropertyChanged(nameof(Time));
            else if (ReferenceEquals(sender, _date))
                OnPropertyChanged(nameof(Date));
            else if (ReferenceEquals(sender, _weather))
                OnPropertyChanged(nameof(Weather));
            else
                OnPropertyChanged((string?)null);
        }
    }
}
