using System.ComponentModel;

namespace FrameIt.ManageFrame
{
    public class WidgetsConfig : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TimeWidget Time { get; } = new();
        public DateWidget Date { get; } = new();
        public WeatherWidget Weather { get; } = new();

        public WidgetsConfig()
        {
            Time.PropertyChanged += Forward;
            Date.PropertyChanged += Forward;
            Weather.PropertyChanged += Forward;
        }

        private void Forward(object sender, PropertyChangedEventArgs e)
            => PropertyChanged?.Invoke(this, e);
    }
}
