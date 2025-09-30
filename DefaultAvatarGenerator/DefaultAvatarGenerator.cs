using System.Drawing;
using System.Drawing.Imaging;

namespace DefaultAvatarGenerator
{
    public static class DefaultAvatarGenerator
    {
        /// <summary>
        /// Draws centered text within a square area.
        /// </summary>
        /// <param name="graphics">The graphics context to draw on.</param>
        /// <param name="text">The text to be drawn. Ignored if null or whitespace.</param>
        /// <param name="edgeLength">The width and height of the square area.</param>
        private static void DrawCenteredText(Graphics graphics, string? text, int edgeLength)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            float fontSize = edgeLength * 0.4f;
            using var font = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            using var brush = new SolidBrush(Color.White);

            var textSize = graphics.MeasureString(text, font);
            var x = (edgeLength - textSize.Width) / 2;
            var y = (edgeLength - textSize.Height) / 2;

            graphics.DrawString(text, font, brush, new PointF(x, y));
        }

        /// <summary>
        /// Generates a square PNG avatar with a solid background color and optional centered text.
        /// </summary>
        /// <param name="backgroundColor">The background color of the avatar.</param>
        /// <param name="text">Optional text to display in the center of the image. Defaults to null.</param>
        /// <param name="edgeLength">Optional size of the avatar in pixels (width and height). Defaults to 500.</param>
        /// <returns>Full file path to the saved PNG file.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when edgeLength is less than or equal to 0.</exception>
        public static string GenerateAvatar(Color backgroundColor, string? text = null, int edgeLength = 500)
        {
            if (edgeLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(edgeLength), "Edge length must be greater than zero.");

            using var bitmap = new Bitmap(edgeLength, edgeLength);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            graphics.Clear(backgroundColor);

            DrawCenteredText(graphics, text, edgeLength);

            string fileName = $"avatar_{Guid.NewGuid()}.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            bitmap.Save(outputPath, ImageFormat.Png);

            return outputPath;
        }

        /// <summary>
        /// Clamps an integer value between 0 and 255 to ensure valid RGB color values.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>A value between 0 and 255.</returns>
        private static int Clamp(int value) => Math.Max(0, Math.Min(255, value));

        /// <summary>
        /// Generates a square PNG avatar with a background color, a soft circular pattern overlay, and optional centered text.
        /// </summary>
        /// <param name="backgroundColor">The background color of the avatar.</param>
        /// <param name="text">Optional text to display in the center of the image. Defaults to null.</param>
        /// <param name="edgeLength">Optional size of the avatar in pixels (width and height). Defaults to 500.</param>
        /// <returns>Full file path to the saved PNG file.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when edgeLength is less than or equal to 0.</exception>
        public static string GenerateAvatarWithPattern(Color backgroundColor, string? text = null, int edgeLength = 500)
        {
            if (edgeLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(edgeLength), "Edge length must be greater than zero.");

            using var bitmap = new Bitmap(edgeLength, edgeLength);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            graphics.Clear(backgroundColor);

            var rand = new Random();
            int patternCount = 50;

            for (int i = 0; i < patternCount; i++)
            {
                int radius = rand.Next(edgeLength / 10, edgeLength / 4);
                int x = rand.Next(0, edgeLength);
                int y = rand.Next(0, edgeLength);

                int variation = rand.Next(-70, 70);
                var patternColor = Color.FromArgb(
                    40,
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

        /// <summary>
        /// Generates a square PNG avatar with a mosaic-style tiled background and optional centered text.
        /// </summary>
        /// <param name="backgroundColor">The base color of the avatar.</param>
        /// <param name="text">Optional text to display in the center of the image. Defaults to null.</param>
        /// <param name="edgeLength">Optional size of the avatar in pixels (width and height). Defaults to 500.</param>
        /// <returns>Full file path to the saved PNG file.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when edgeLength is less than or equal to 0.</exception>
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

        /// <summary>
        /// Generates a square PNG avatar with vertical bar chart-like elements over a background and optional centered text.
        /// </summary>
        /// <param name="backgroundColor">The base color of the avatar.</param>
        /// <param name="text">Optional text to display in the center of the image. Defaults to null.</param>
        /// <param name="edgeLength">Optional size of the avatar in pixels (width and height). Defaults to 500.</param>
        /// <returns>Full file path to the saved PNG file.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when edgeLength is less than or equal to 0.</exception>
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
                int barHeight = rand.Next(edgeLength / 4, edgeLength); // Random height
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
