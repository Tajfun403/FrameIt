using CommunityToolkit.Mvvm.ComponentModel;
using FrameIt.Models;
using System.ComponentModel;

namespace FrameIt
{
    public class WidgetsConfig : ObservableObject
    {
        private TimeWidget _time = new TimeWidget();
        private DateWidget _date = new DateWidget();
        private WeatherWidget _weather = new WeatherWidget();

        public WidgetsConfig()
        {
            // Subscribe to the default nested widget instances so their property changes
            // bubble up as WidgetsConfig.PropertyChanged.
            SubscribeToWidget(_time);
            SubscribeToWidget(_date);
            SubscribeToWidget(_weather);
        }

        public TimeWidget Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (ReferenceEquals(_time, value)) return;
                UnsubscribeFromWidget(_time);
                _time = value;
                SubscribeToWidget(_time);
                OnPropertyChanged(nameof(Time));
            }
        }

        public DateWidget Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (ReferenceEquals(_date, value)) return;
                UnsubscribeFromWidget(_date);
                _date = value;
                SubscribeToWidget(_date);
                OnPropertyChanged(nameof(Date));
            }
        }

        public WeatherWidget Weather
        {
            get
            {
                return _weather;
            }
            set
            {
                if (ReferenceEquals(_weather, value)) return;
                UnsubscribeFromWidget(_weather);
                _weather = value;
                SubscribeToWidget(_weather);
                OnPropertyChanged(nameof(Weather));
            }
        }

        private void SubscribeToWidget(object? widget)
        {
            if (widget is INotifyPropertyChanged npc)
            {
                npc.PropertyChanged += NestedWidget_PropertyChanged;
            }
        }

        private void UnsubscribeFromWidget(object? widget)
        {
            if (widget is INotifyPropertyChanged npc)
            {
                npc.PropertyChanged -= NestedWidget_PropertyChanged;
            }
        }

        private void NestedWidget_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (ReferenceEquals(sender, _time))
            {
                OnPropertyChanged(nameof(Time));
            }
            else if (ReferenceEquals(sender, _date))
            {
                OnPropertyChanged(nameof(Date));
            }
            else if (ReferenceEquals(sender, _weather))
            {
                OnPropertyChanged(nameof(Weather));
            }
            else
            {
                OnPropertyChanged();
            }
        }
    }
}