using System;

namespace FrameIt.Models
{
    public class FrameConfig
    {
        public string Name { get; set; } = "My Frame";
        public bool IsFrameOn { get; set; } = true;
        public int Brightness { get; set; } = 100;

        public bool IsAutoScheduleEnabled { get; set; }

        public DateTime? TurnOffTime { get; set; }
        public DateTime? TurnOnTime { get; set; }

        public WidgetsConfig Widgets { get; set; } = new();
    }

    public class WidgetsConfig
    {
        public TimeWidget Time { get; set; } = new();
        public DateWidget Date { get; set; } = new();
        public WeatherWidget Weather { get; set; } = new();
    }

    public class TimeWidget
    {
        public bool IsEnabled { get; set; }
        public string TimeZone { get; set; } = "Europe/Warsaw";
    }

    public class DateWidget
    {
        public bool IsEnabled { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }

    public class WeatherWidget
    {
        public bool IsEnabled { get; set; }
        public string Location { get; set; } = "Kraków";
    }
}
