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

            if (!string.IsNullOrWhiteSpace(text))
            {
                // Choose font size proportional to image size
                float fontSize = edgeLength * 0.4f;
                using var font = new System.Drawing.Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
                using var brush = new SolidBrush(Color.White);

                // Measure text size
                var textSize = graphics.MeasureString(text, font);
                var x = (edgeLength - textSize.Width) / 2;
                var y = (edgeLength - textSize.Height) / 2;

                graphics.DrawString(text, font, brush, new PointF(x, y));
            }

            // Generate unique file name
            string fileName = $"avatar_{Guid.NewGuid()}.png";
            string outputPath = Path.Combine(Path.GetTempPath(), fileName);

            bitmap.Save(outputPath, ImageFormat.Png);

            return outputPath;
        }
    }
}
