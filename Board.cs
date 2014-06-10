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
using System.Linq;
using System.Text;


namespace _2048
{
    public class Board : IStatefullBoard
    {
        private readonly SByte m_size;
        private List<ITile> m_board;
        private readonly Random m_random = new Random();
        private readonly Stack<List<ITile>> m_undoboards = new Stack<List<ITile>>();

        public int Score { get; private set; }
        public int StepsCount { get; private set; }

        public Board(SByte size = 4)
        {
            m_size = size;
            m_board = new List<ITile>(m_size * m_size);
            sbyte y = 0;
            for (SByte x = 0; x < m_size * m_size; x++)
            {
                if ((x % m_size == 0) && (x > 0))
                    y++;

                m_board.Add(new Tile { X = y, Y = (sbyte)(x - (sbyte)(y * m_size)) });
            }
            NextFill();
        }

        public List<ITile> DeepCopy()
        {
            var board = new List<ITile>(m_size * m_size);
            foreach (var tile in m_board)
                board.Add(new Tile() { X = tile.X, Y = tile.Y, Value = tile.Value });
            return board;
        }

        bool HasEqualInVector(Func<ITile, int> predicate)
        {
            for (int c = 0; c < m_size; c++)
            {
                var col1 = new List<ITile>(m_board.Where(x => predicate(x) == c));
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

        IEnumerable<ITile> GetEmptyTiles()
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
	    tile.Value = (m_random.Next(100) % 10) != 0 ? 2 : 4;// 90% - 2, 10% - 4
            return true;
        }

        public ITile[,] To2DArray()
        {
            var output = new ITile[m_size, m_size];

            foreach (var tile in m_board)
            {
                output[tile.X, tile.Y] = tile;
            }
            return output;
        }

        private void Move(Func<ITile, int> predicate, bool up)
        {
            m_undoboards.Push(DeepCopy());
            StepsCount = StepsCount + 1;
            for (int c = 0; c < m_size; c++)
            {
                var col1 = new List<ITile>(m_board.Where(x => predicate(x) == c));

                for (int i = 0; i < m_size - 1; i++)
                    if (up)
                        MoveP(col1);
                    else
                        MoveN(col1);
            }
            NextFill();
        }


        private void MoveP(List<ITile> col1)
        {
            for (sbyte y1 = 0; y1 < m_size - 1; y1++)
            {
                if (col1[y1].Value == col1[y1 + 1].Value || col1[y1].Value == 0)
                {
                    if (col1[y1].Value == col1[y1 + 1].Value)
                        Score += col1[y1].Value;
                    col1[y1].Value = col1[y1].Value + col1[y1 + 1].Value;
                    col1[y1 + 1].Value = 0;
                }
            }
        }

        private void MoveN(List<ITile> col1)
        {
            for (sbyte y1 = (sbyte)(m_size - 1); y1 > 0; y1--)
            {
                if (col1[y1].Value == col1[y1 - 1].Value || col1[y1].Value == 0)
                {
                    if (col1[y1].Value == col1[y1 - 1].Value)
                        Score += col1[y1].Value;
                    col1[y1].Value = col1[y1].Value + col1[y1 - 1].Value;
                    col1[y1 - 1].Value = 0;
                }
            }
        }

        public void MoveUp()
        {
            Move(x => x.Y, true);
        }

        public void MoveDown()
        {
            Move(x => x.Y, false);
        }

        public void MoveLeft()
        {
            Move(x => x.X, true);
        }

        public void MoveRight()
        {
            Move(x => x.X, false);
        }


        public void Undo()
        {
            if (StepsCount < 1)
                return;
            m_board = new List<ITile>(m_undoboards.Pop().ToArray());
            StepsCount = StepsCount - 1;
        }
    }
}

