namespace FrameIt.Models
{
    public class TimeWidget : WidgetBase
    {
        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _timeZone = string.Empty;
        public string TimeZone
        {
            get
            {
                return _timeZone;
            }
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
}

