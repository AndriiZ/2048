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
using System.Collections;
using System.Text;

namespace _2048
{
    class _2048Options : IGameOptions
    {
	public int MaxStepCount { get { return 1000; } }	    
    }

    class Program
    {
	static IGameEngine GetGameEngine()
	{
            IGameEngine engine = null;
            while (engine == null)
            {
                Console.Clear();
                Console.WriteLine("Select mode:");
                Console.WriteLine("1. User game");
                Console.WriteLine("2. AI game");
                var key = Console.ReadKey(true).KeyChar.ToString();
                if (key == "1")
                    engine = new ConsoleUserEngine();
                if (key == "2")
                    engine = new AINaiveEngine();
            }
	    return engine;
	}

		static void GamePlay(IGameEngine engine, IGameOptions options, IStatefullBoard board, TileColorizer colorizer)
		{
			bool running = true;
            while (running)
            {
                NextStepCommand command = engine.GetNextStep(board.To2DArray());

                switch (command)
                {
                    case NextStepCommand.Up : 
                        board.MoveUp();
                        break;
                    case NextStepCommand.Down:
                        board.MoveDown();
                        break;
                    case NextStepCommand.Right:
                        board.MoveRight();
                        break;
                    case NextStepCommand.Left:
                        board.MoveLeft();
                        break;
                    case NextStepCommand.Undo:
                        if (!engine.IsAI())
							board.Undo();
                        break;
                    case NextStepCommand.Break:
                        running = false;
                        break;
                }

                if (running)
                    PrintBoard(board, colorizer);
                if (!board.NextStepAvailable())
                {
                    running = false;
                }
				if (running && engine.IsAI() && board.StepsCount > options.MaxStepCount)
				{	
					running = false;
				}
            }
		}
	
        static void Main(string[] args)
        {
            IGameEngine engine = GetGameEngine();
			IGameOptions options = new _2048Options();
            IStatefullBoard board = new Board();
            var colorizer = new TileColorizer();

            Console.BackgroundColor = ConsoleColor.White;
            PrintBoard(board, colorizer);
            GamePlay(engine, options, board, colorizer);
            Console.WriteLine("Game over!");
			if (engine.IsAI() && board.StepsCount > options.MaxStepCount)
			{	
				Console.WriteLine("Halt! {0} step limit reached!", options.MaxStepCount);
			}

			Console.ResetColor();
        }

        private static void PrintBoard(IBoard board, TileColorizer colorizer)
        {
            Console.Clear();
            Console.WriteLine("Used {0,4} steps, score {1,5}", board.StepsCount, board.Score);
            var arr = board.To2DArray();
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.ForegroundColor = colorizer.GetColorByValue(arr[i, j].Value);
                    Console.Write("{0,4} ", arr[i, j].Value);
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Press q to exit, use arrow keys for game, ctrl+z to undo");
        }
    }
}

