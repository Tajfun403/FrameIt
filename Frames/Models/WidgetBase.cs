using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FrameIt.Models
{
    public abstract class WidgetBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
