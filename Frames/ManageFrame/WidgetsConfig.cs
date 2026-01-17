using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FrameIt
{
    public class WidgetsConfig : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler? PropertyChanged;

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
                OnPropertyChanged(null);
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}