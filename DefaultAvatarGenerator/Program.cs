using System.Drawing;

namespace DefaultAvatarGenerator
{
    public class Program
    {
        public static void Main()
        {
            string initials = "JD";
            int seed = initials.GetHashCode(); // Could also use user ID, etc.

            Color userColor = GetRandomColor();

            string filePath = DefaultAvatarGenerator.GenerateAvatarWithPattern(userColor, initials);
            Console.WriteLine($"Seed is: {seed}");
            Console.WriteLine($"Avatar generated at: {filePath}");
        }

        public static Color GetRandomColor(int seed = 0)
        {
            var colors = new List<Color>
            {
                Color.FromArgb(52, 152, 219),   // Blue (#3498db)
                Color.FromArgb(231, 76, 60),    // Red (#e74c3c)
                Color.FromArgb(46, 204, 113),   // Green (#2ecc71)
                Color.FromArgb(241, 196, 15),   // Yellow (#f1c40f)
                Color.FromArgb(155, 89, 182),   // Purple (#9b59b6)
                Color.FromArgb(26, 188, 156),   // Teal (#1abc9c)
                Color.FromArgb(233, 30, 99),    // Pink (#e91e63)
            };

            int index = Math.Abs(seed % colors.Count);
            return colors[index];
        }
    }
}