using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace FrameIt.Frames
{
    public class FramesManager
    {
        private const string FramesFile = "Data/frames.json";

        public ObservableCollection<FrameItem> Frames { get; private set; }

        public FramesManager()
        {
            LoadFrames();
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

        public void SaveFrames()
        {
            var json = JsonSerializer.Serialize(Frames, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FramesFile, json);
        }

        public void AddFrame(FrameItem frame)
        {
            
        }
    }
}
