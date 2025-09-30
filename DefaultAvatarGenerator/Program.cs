using System.Drawing;

namespace DefaultAvatarGenerator
{
    public class Program
    {
        public static void Main()
        {
            string initials = "JD";

            // Get a consistent color based on initials.
            Color userColor = ColorPicker.GetRandomColor(initials);

            // Generate an avatar image composed of background color, and centered text.
            string filePath = DefaultAvatarGenerator.GenerateAvatarWithPattern(userColor, initials);

            Console.WriteLine($"Avatar generated at: {filePath}");
        }
    }
}