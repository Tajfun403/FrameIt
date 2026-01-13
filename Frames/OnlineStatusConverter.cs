namespace FrameIt.Frames
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class OnlineStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            return value is bool b && b ? "ONLINE" : "OFFLINE";
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
