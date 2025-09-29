using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DefaultAvatarGenerator
{
    public static class DefaultAvatarGenerator
    {
        private static void DrawCenteredText(Graphics graphics, string? text, int edgeLength)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            float fontSize = edgeLength * 0.4f;
            using var font = new System.Drawing.Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            using var brush = new SolidBrush(Color.White);

            var textSize = graphics.MeasureString(text, font);
            var x = (edgeLength - textSize.Width) / 2;
            var y = (edgeLength - textSize.Height) / 2;

            graphics.DrawString(text, font, brush, new PointF(x, y));
        }

        /// <summary>
        /// Generates a square PNG avatar with a background color and optional centered text.
        /// </summary>
        /// <param name="backgroundColor">The background color of the image (System.Drawing.Color).</param>
        /// <param name="text">Optional text to center on the image. Defaults to null.</param>
        /// <param name="edgeLength">Optional edge length in pixels. Defaults to 500.</param>
        /// <returns>Full path to the saved PNG file.</returns>
        public static string GenerateAvatar(Color backgroundColor, string? text = null, int edgeLength = 500)
        {
            // Sanity check
            if (edgeLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(edgeLength), "Edge length must be greater than zero.");

            using var bitmap = new Bitmap(edgeLength, edgeLength);
            using var graphics = Graphics.FromImage(bitmap);

            // High quality settings
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            graphics.Clear(backgroundColor);

            DrawCenteredText(graphics, text, edgeLength);

            // Generate unique file name
            string fileName = $"avatar_{Guid.NewGuid()}.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            bitmap.Save(outputPath, ImageFormat.Png);

            return outputPath;
        }

        /// <summary>
        /// Helper to keep RGB values in range
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int Clamp(int value) => Math.Max(0, Math.Min(255, value));

        public static string GenerateAvatarWithPattern(Color backgroundColor, string? text = null, int edgeLength = 500)
        {
            if (edgeLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(edgeLength), "Edge length must be greater than zero.");

            using var bitmap = new Bitmap(edgeLength, edgeLength);
            using var graphics = Graphics.FromImage(bitmap);

            // High quality settings
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            // Fill background
            graphics.Clear(backgroundColor);

            var rand = new Random();
            int patternCount = 50; // Adjust for density

            for (int i = 0; i < patternCount; i++)
            {
                int radius = rand.Next(edgeLength / 10, edgeLength / 4);
                int x = rand.Next(0, edgeLength);
                int y = rand.Next(0, edgeLength);

                int variation = rand.Next(-70, 70);
                var patternColor = Color.FromArgb(
                    40, // very low alpha
                    Clamp(backgroundColor.R + variation),
                    Clamp(backgroundColor.G + variation),
                    Clamp(backgroundColor.B + variation)
                );

                using var brush = new SolidBrush(patternColor);
                graphics.FillEllipse(brush, x - radius / 2, y - radius / 2, radius, radius);
            }

            DrawCenteredText(graphics, text, edgeLength);

            string fileName = $"avatar_pattern_{Guid.NewGuid()}.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            bitmap.Save(outputPath, ImageFormat.Png);

            return outputPath;
        }

        public static string GenerateAvatarWithMosaic(Color backgroundColor, string? text = null, int edgeLength = 500)
        {
            if (edgeLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(edgeLength), "Edge length must be greater than zero.");

            using var bitmap = new Bitmap(edgeLength, edgeLength);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            int tileSize = edgeLength / 10; // 10x10 tiles
            var rand = new Random();

            for (int y = 0; y < edgeLength; y += tileSize)
            {
                for (int x = 0; x < edgeLength; x += tileSize)
                {
                    int variation = rand.Next(-25, 25);
                    var tileColor = Color.FromArgb(
                        Clamp(backgroundColor.R + variation),
                        Clamp(backgroundColor.G + variation),
                        Clamp(backgroundColor.B + variation)
                    );

                    using var brush = new SolidBrush(tileColor);
                    graphics.FillRectangle(brush, x, y, tileSize, tileSize);
                }
            }

            DrawCenteredText(graphics, text, edgeLength);

            string fileName = $"avatar_mosaic_{Guid.NewGuid()}.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            bitmap.Save(outputPath, ImageFormat.Png);

            return outputPath;
        }

        public static string GenerateAvatarWithTradingBars(Color backgroundColor, string? text = null, int edgeLength = 500)
        {
            if (edgeLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(edgeLength), "Edge length must be greater than zero.");

            using var bitmap = new Bitmap(edgeLength, edgeLength);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            graphics.Clear(backgroundColor);

            var rand = new Random();
            int barWidth = edgeLength / 25;
            int spacing = edgeLength / 20;

            for (int i = 0; i < edgeLength; i += spacing)
            {
                int barHeight = rand.Next(edgeLength / 4, edgeLength / 1); // Random height
                int x = i;
                int y = edgeLength - barHeight;

                int variation = rand.Next(-70, 70);
                var barColor = Color.FromArgb(
                    60,
                    Clamp(backgroundColor.R + variation),
                    Clamp(backgroundColor.G + variation),
                    Clamp(backgroundColor.B + variation)
                );

                using var brush = new SolidBrush(barColor);
                graphics.FillRectangle(brush, x, y, barWidth, barHeight);
            }

            DrawCenteredText(graphics, text, edgeLength);

            string fileName = $"avatar_tradingbars_{Guid.NewGuid()}.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            bitmap.Save(outputPath, ImageFormat.Png);

            return outputPath;
        }
    }
}
