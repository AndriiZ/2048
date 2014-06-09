using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    public class Tile
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


        public int Value { get; set; }
        public ConsoleColor Color
        {
            get
            {
                ConsoleColor color = ConsoleColor.Black;
                colors.TryGetValue(Value, out color);
                return color;
            }
        }
        public SByte X { get; set; }
        public SByte Y { get; set; }
    }

    public class Board
    {
        private readonly SByte m_size;
        private List<Tile> m_board;
        private Random m_random = new Random();

        public Board(SByte size = 4)
        {
            m_size = size;
            m_board = new List<Tile>(m_size * m_size);
            sbyte y = 0;
            for (SByte x = 0; x < m_size * m_size; x++)
            {
                if ((x % m_size == 0) && (x > 0))
                    y++;

                m_board.Add(new Tile { X = y, Y = (sbyte)(x - (sbyte)(y * m_size)) });
            }
            NextFill();
        }

        bool HasEqualInVector(Func<Tile, int> predicate)
        {
            for (int c = 0; c < m_size; c++)
            {
                var col1 = new List<Tile>(m_board.Where(x => predicate(x) == c));
                for (int i = 0; i < m_size - 1; i++)
                    if (col1[i].Value == col1[i + 1].Value)
                        return true;
            }
            return false;
        }

        public bool NextStepAvailable()
        {
            return GetEmptyTiles().Any() || HasEqualInVector(x => x.Y) || HasEqualInVector(x => x.X);
        }

        IEnumerable<Tile> GetEmptyTiles()
        {
            return m_board.Where(x => x.Value == 0);
        }

        bool NextFill()
        {
            var emptyTiles = GetEmptyTiles();
            int emptyCount = emptyTiles.Count();
            if (emptyCount == 0)
                return false;
            int cellNumber = m_random.Next(emptyCount);
            var tile = emptyTiles.Skip(cellNumber).First();
            tile.Value = 2;
            return true;
        }

        public Tile[,] To2DArray()
        {
            var output = new Tile[m_size, m_size];

            foreach (var tile in m_board)
            {
                output[tile.X, tile.Y] = tile;
            }
            return output;
        }

        private void MoveP(Func<Tile, int> predicate)
        {
            for (int c = 0; c < m_size; c++)
            {
                var col1 = new List<Tile>(m_board.Where(x => predicate(x) == c));

                for (int i = 0; i < m_size - 1; i++)
                    for (sbyte y1 = 0; y1 < m_size - 1; y1++)
                    {
                        if (col1[y1].Value == col1[y1 + 1].Value || col1[y1].Value == 0)
                        {
                            col1[y1].Value = col1[y1].Value + col1[y1 + 1].Value;
                            col1[y1 + 1].Value = 0;
                        }
                    }
            }
        }

        private void MoveN(Func<Tile, int> predicate)
        {
            for (int c = 0; c < m_size; c++)
            {
                var col1 = new List<Tile>(m_board.Where(x => predicate(x) == c));

                for (int i = 0; i < m_size - 1; i++)
                    for (sbyte y1 = (sbyte)(m_size - 1); y1 > 0; y1--)
                    {
                        if (col1[y1].Value == col1[y1 - 1].Value || col1[y1].Value == 0)
                        {
                            col1[y1].Value = col1[y1].Value + col1[y1 - 1].Value;
                            col1[y1 - 1].Value = 0;
                        }
                    }
            }
        }

        public void MoveUp()
        {
            MoveP(x => x.Y);
            NextFill();
        }

        public void MoveDown()
        {
            MoveN(x => x.Y);
            NextFill();
        }

        public void MoveLeft()
        {
            MoveP(x => x.X);
            NextFill();
        }

        public void MoveRight()
        {
            MoveN(x => x.X);
            NextFill();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            bool running = true;
            Console.BackgroundColor = ConsoleColor.White;
            PrintBoard(board);

            while (running)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        board.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        board.MoveDown();
                        break;
                    case ConsoleKey.LeftArrow:
                        board.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        board.MoveRight();
                        break;
                    case ConsoleKey.Q:
                        running = false;
                        break;
                }
                if (running)
                    PrintBoard(board);
                if (!board.NextStepAvailable())
                    Console.WriteLine("Game over!");
            }
        }

        private static void PrintBoard(Board board)
        {
            Console.Clear();
            var arr = board.To2DArray();
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.ForegroundColor = arr[i, j].Color;
                    Console.Write(string.Format("{0,4} ", arr[i, j].Value));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.WriteLine("Press q to exit, use arrou keys for game");
        }
    }
}

