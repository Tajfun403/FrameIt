using System;
using FrameIt;

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

        // Use the single WidgetsConfig implementation that implements INotifyPropertyChanged
        public WidgetsConfig Widgets { get; set; } = new();
    }
}
