using FrameIt.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FrameIt.Services
{
    public static class FramesManager
    {
        private static readonly string FilePath = "frames.json";

        public static List<FrameItem> LoadFrames()
        {
            if (!File.Exists(FilePath))
                return new List<FrameItem>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<FrameItem>>(json) ?? new();
        }

        public static void SaveFrame(FrameItem frame)
        {
            var frames = LoadFrames();
            var index = frames.FindIndex(f => f.Id == frame.Id);

            if (index >= 0)
                frames[index] = frame;
            else
                frames.Add(frame);

            File.WriteAllText(
                FilePath,
                JsonSerializer.Serialize(
                    frames,
                    new JsonSerializerOptions { WriteIndented = true }
                )
            );
        }

        public static int GenerateNextId()
        {
            var frames = LoadFrames();
            return frames.Count == 0 ? 1 : frames.Max(f => f.Id) + 1;
        }

        public static void DeleteFrame(int frameId)
        {
            var frames = LoadFrames();
            var frame = frames.FirstOrDefault(f => f.Id == frameId);

            if (frame != null)
            {
                frames.Remove(frame);
                File.WriteAllText(FilePath,
                    JsonSerializer.Serialize(frames, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }));
            }

        }
    }
}
