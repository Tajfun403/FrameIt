using FrameIt.Models;

public class TimeWidget : WidgetBase
{
    private bool _isEnabled;
    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            if (_isEnabled != value)
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }
    }

    private string _timeZone;
    public string TimeZone
    {
        get => _timeZone;
        set
        {
            if (_timeZone != value)
            {
                _timeZone = value;
                OnPropertyChanged();
            }
        }
    }
}
