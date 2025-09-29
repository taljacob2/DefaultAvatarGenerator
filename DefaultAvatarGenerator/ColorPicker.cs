using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAvatarGenerator
{
    public class ColorPicker
    {
        private static int GetStableHash(string input)
        {
            unchecked
            {
                int hash = 23;
                foreach (char c in input)
                {
                    hash = hash * 31 + c;
                }
                return hash;
            }
        }

        public static Color GetRandomColor(string? seed = null)
        {
            int intSeed = seed == null ? 0 : GetStableHash(seed);

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

            int index = Math.Abs(intSeed % colors.Count);
            return colors[index];
        }
    }
}
