using CommunityToolkit.Mvvm.Input;
using FrameIt.Models;
using FrameIt.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class ManageFrameViewModel : INotifyPropertyChanged
{
    private readonly FrameItem _frame;
    public int FrameId => _frame.Id;

    public ICommand DeleteFrameCommand { get; }

    public ManageFrameViewModel(FrameItem frame)
    {
        _frame = frame;

        _frameName = frame.Config.Name;

        TimeZones = TimeZoneInfo.GetSystemTimeZones()
                                .Select(tz => tz.Id)
                                .ToList();

        DeleteFrameCommand = new RelayCommand(DeleteFrame);
    }

    // ================= FRAME NAME =================

    private string _frameName;
    public string FrameName
    {
        get => _frameName;
        set
        {
            if (_frameName != value)
            {
                _frameName = value;
                _frame.Config.Name = value;
                Save();
                OnPropertyChanged();
            }
        }
    }

    // ================= BASIC CONFIG =================

    public int Brightness
    {
        get => _frame.Config.Brightness;
        set
        {
            if (_frame.Config.Brightness != value)
            {
                _frame.Config.Brightness = value;
                Save();
                OnPropertyChanged();
            }
        }
    }

    public bool IsFrameOn
    {
        get => _frame.Config.IsFrameOn;
        set
        {
            if (_frame.Config.IsFrameOn != value)
            {
                _frame.Config.IsFrameOn = value;
                Save();
                OnPropertyChanged();
            }
        }
    }

    // ================= SCHEDULE =================

    public bool IsAutoScheduleEnabled
    {
        get => _frame.Config.IsAutoScheduleEnabled;
        set
        {
            if (_frame.Config.IsAutoScheduleEnabled != value)
            {
                _frame.Config.IsAutoScheduleEnabled = value;
                Save();
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsScheduleVisible));
            }
        }
    }

    public bool IsScheduleVisible => IsAutoScheduleEnabled;

    public DateTime? TurnOffTime
    {
        get => _frame.Config.TurnOffTime;
        set
        {
            if (_frame.Config.TurnOffTime != value)
            {
                _frame.Config.TurnOffTime = value;
                Save();
                OnPropertyChanged();
            }
        }
    }

    public DateTime? TurnOnTime
    {
        get => _frame.Config.TurnOnTime;
        set
        {
            if (_frame.Config.TurnOnTime != value)
            {
                _frame.Config.TurnOnTime = value;
                Save();
                OnPropertyChanged();
            }
        }
    }

    public WidgetsConfig Widgets => _frame.Config.Widgets;


    public List<string> TimeZones { get; }

    private void Save()
    {
        FramesManager.SaveFrame(_frame);
    }

    private void DeleteFrame()
    {
        FramesManager.DeleteFrame(_frame.Id);
    }


    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
