using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameIt.Frames
{
    /// <summary>
    /// Interaction logic for FramesMain.xaml
    /// </summary>
    public partial class FramesMain : Page
    {

        public ObservableCollection<FrameItem> Frames { get; set; }
        private const string FramesFile = "Data/frames.json";

        public FramesMain()
        {
            InitializeComponent();
            LoadFrames();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadFrames()
        {
            if (!File.Exists(FramesFile))
            {
                Frames = new ObservableCollection<FrameItem>();
                SaveFrames();
                return;
            }

            var json = File.ReadAllText(FramesFile);
            Frames = JsonSerializer.Deserialize<ObservableCollection<FrameItem>>(json)
                     ?? new ObservableCollection<FrameItem>();
        }

        private void SaveFrames()
        {
            var json = JsonSerializer.Serialize(Frames, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FramesFile, json);
        }
    }
}

    
