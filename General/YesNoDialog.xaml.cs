using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameIt.General
{
    /// <summary>
    /// Interaction logic for YesNoDialog.xaml
    /// </summary>
    public partial class YesNoDialog : UserControl, INotifyPropertyChanged
    {
        public YesNoDialog()
        {
            InitializeComponent();
        }

        public string Title
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        } = "Title";

        public string MainText { get; set
            {
                field = value;
                OnPropertyChanged();
            }
        } = "Main text.";

        public bool IsYesPositive
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsYesNegative));
            }
        }
         = false;

        public bool IsYesNegative => !IsYesPositive;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            PopUpManager.ReturnDialogResult(false);
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            PopUpManager.ReturnDialogResult(true);
        }
    }
}
