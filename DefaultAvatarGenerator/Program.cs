using System.Drawing;

namespace DefaultAvatarGenerator
{
    public class Program
    {
        public static void Main()
        {
            string initials = "JD";

            Color userColor = ColorPicker.GetRandomColor(initials);

            string filePath = DefaultAvatarGenerator.GenerateAvatarWithPattern(userColor, initials);
            Console.WriteLine($"Avatar generated at: {filePath}");
        }
    }
}