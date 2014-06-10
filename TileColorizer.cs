/* 
    2048 Game implementation by Andrii Zhuk
    
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;

namespace _2048
{
    public class TileColorizer
    {
        private readonly Dictionary<int, ConsoleColor> colors = new Dictionary<int, ConsoleColor>
        {
            {2, ConsoleColor.Black},
            {4, ConsoleColor.Gray},
            {8, ConsoleColor.Green},
            {16, ConsoleColor.DarkGreen},
            {32, ConsoleColor.Magenta},
            {64, ConsoleColor.DarkMagenta},
            {128, ConsoleColor.Cyan},
            {256, ConsoleColor.DarkCyan},
            {512, ConsoleColor.Red},
            {1024, ConsoleColor.DarkRed},
            {2048, ConsoleColor.Blue},
            {4096, ConsoleColor.DarkBlue},
            {8192, ConsoleColor.Yellow},
            {16384, ConsoleColor.DarkYellow}
        };

        public ConsoleColor GetColorByValue(int value)
        {
            ConsoleColor color;
            if (colors.TryGetValue(value, out color))
                return color;
            return ConsoleColor.Black;
        }
    }
}

