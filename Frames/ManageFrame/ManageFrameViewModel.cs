using FrameIt.Models;
using FrameIt.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FrameIt.Frames.ManageFrame
{
    public class ManageFrameViewModel : INotifyPropertyChanged
    {
        public bool ShowNavigation => true;
        private readonly FrameItem _frame;

        public ManageFrameViewModel(FrameItem frame)
        {
            _frame = frame;

            TimeZones = TimeZoneInfo.GetSystemTimeZones()
                                    .Select(tz => tz.Id)
                                    .ToList();
        }

        // =======================
        // BASIC
        // =======================

        public string FrameName
        {
            get => _frame.Config.Name;
            set
            {
                _frame.Config.Name = value;
                Save();
                OnPropertyChanged();
            }
        }

        public int Brightness
        {
            get => _frame.Config.Brightness;
            set
            {
                _frame.Config.Brightness = value;
                Save();
                OnPropertyChanged();
            }
        }

        // =======================
        // POWER
        // =======================

        public bool IsFrameOn
        {
            get => _frame.Config.IsFrameOn;
            set
            {
                _frame.Config.IsFrameOn = value;
                Save();
                OnPropertyChanged();
            }
        }

        // =======================
        // AUTO SCHEDULE
        // =======================

        public bool IsAutoScheduleEnabled
        {
            get => _frame.Config.IsAutoScheduleEnabled;
            set
            {
                _frame.Config.IsAutoScheduleEnabled = value;
                Save();
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsScheduleVisible));
            }
        }

        public bool IsScheduleVisible => IsAutoScheduleEnabled;

        public DateTime? TurnOffTime
        {
            get => _frame.Config.TurnOffTime;
            set
            {
                _frame.Config.TurnOffTime = value;
                Save();
                OnPropertyChanged();
            }
        }

        public DateTime? TurnOnTime
        {
            get => _frame.Config.TurnOnTime;
            set
            {
                _frame.Config.TurnOnTime = value;
                Save();
                OnPropertyChanged();
            }
        }

        // =======================
        // WIDGETS
        // =======================

        public WidgetsConfig Widgets => _frame.Config.Widgets;

        public List<string> TimeZones { get; }

        // =======================
        // SAVE
        // =======================

        private void Save()
        {
            FramesManager.SaveFrame(_frame);
        }

        // =======================
        // INotifyPropertyChanged
        // =======================

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
