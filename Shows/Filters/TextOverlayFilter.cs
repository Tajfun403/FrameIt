using System;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Windows.Media.Imaging;

namespace FrameIt.Shows.Filters
{
    internal class TextOverlayFilter : IFilterBase
    {
        public string? Text { get; set; }

        public TextOverlayFilter(string text)
        {
            Text = text;
        }

        public TextOverlayFilter() : this("Sample Text")
        {
        }

        public BitmapImage ApplyFilter(BitmapImage source)
        {
            if (String.IsNullOrEmpty(Text?.Trim()))
            {
                return source;
            }

            using var img = IFilterBase.ToImageSharp(source);

            int width = img.Width;
            int height = img.Height;

            // Bottom 30% overlay
            int overlayHeight = (int)(height * 0.3);
            var overlayRect = new Rectangle(0, height - overlayHeight, width, overlayHeight);

            img.Mutate(ctx =>
            {
                // Fill overlay
                ctx.Fill(Color.Black.WithAlpha(0.5f), overlayRect);

                // Load system font
                Font font = SystemFonts.CreateFont("Arial", overlayHeight / 4f);

                // Create TextOptions for wrapped, centered text
                var textOptions = new SixLabors.ImageSharp.Drawing.Processing.RichTextOptions(font)//  TextOptions(font)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Origin = new PointF(width / 2f, height - overlayHeight / 2f),
                    // WrappingWidth = width - 20 // horizontal padding
                };

                // Draw text
                ctx.DrawText(textOptions, Text, Color.White);
            });

            return IFilterBase.ToBitmapImage(img);
        }
    }
}
