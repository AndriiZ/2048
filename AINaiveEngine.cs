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
using System.Threading;

namespace _2048
{
    public class AINaiveEngine : IGameEngine
    {
        private Random m_random;
        public AINaiveEngine()
        {
            m_random = new Random();
        }

		public bool IsAI()
		{
			return true;
		}

        public NextStepCommand GetNextStep(ITile[,] board)
        {
            var command = NextStepCommand.Nop;
            switch (m_random.Next() % 4)
            {
                case 0 : command = NextStepCommand.Up;
                    break;
                case 1: command = NextStepCommand.Down;
                    break;
                case 2: command = NextStepCommand.Right;
                    break;
                case 3: command = NextStepCommand.Left;
                    break;
            }
            Thread.Sleep(300);
            return command;
        }
    }
}

