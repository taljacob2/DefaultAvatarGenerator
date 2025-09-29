using System.Drawing;

namespace DefaultAvatarGenerator
{
    public class Program
    {
        public static void Main()
        {
            string initials = "JD";
            Color userColor = Color.FromArgb(52, 152, 219); // A blue color (hex: #3498db)

            string filePath = DefaultAvatarGenerator.GenerateAvatar(userColor, initials);
            Console.WriteLine($"Avatar generated at: {filePath}");
        }
    }
}