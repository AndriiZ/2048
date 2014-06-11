#region Header

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

#endregion Header

namespace _2048
{
    using System;
    using _2048Lib;

    public sealed class ConsoleUserEngine : IGameEngine
    {
        #region Methods

        public NextStepCommand GetNextStep(ITile[,] board)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    return NextStepCommand.Up;
                case ConsoleKey.DownArrow:
                    return NextStepCommand.Down;
                case ConsoleKey.LeftArrow:
                    return NextStepCommand.Left;
                case ConsoleKey.RightArrow:
                    return NextStepCommand.Right;
                case ConsoleKey.Q:
                    return NextStepCommand.Break;
                case ConsoleKey.Z:
                    if ((key.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                        return NextStepCommand.Break;
                    break;
            }
            return NextStepCommand.Nop;
        }

        public bool IsAI()
        {
            return false;
        }

        #endregion Methods
    }
}
