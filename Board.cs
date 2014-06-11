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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Board : IStatefullBoard
    {
        #region Fields

        private readonly Random m_random = new Random();
        private readonly SByte m_size;
        private readonly Stack<List<Tile>> m_undoboards = new Stack<List<Tile>>();

        private List<Tile> m_board;

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Properties

        public int Score
        {
            get; private set;
        }

        public int StepsCount
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

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

        public void MoveUp()
        {
            Move(x => x.Y, true);
        }

        public bool NextStepAvailable()
        {
            return GetEmptyTiles().Any() || HasEqualInVector(x => x.Y) || HasEqualInVector(x => x.X);
        }

        public ITile[,] To2DArray()
        {
            var output = new ITile[m_size, m_size];

            foreach (var tile in m_board)
            {
                output[tile.X, tile.Y] = new Tile { X = tile.X, Y = tile.Y, Value = tile.Value };
            }
            return output;
        }

        public void Undo()
        {
            if (StepsCount < 1)
                return;
            m_board = new List<Tile>(m_undoboards.Pop().ToArray());
            StepsCount = StepsCount - 1;
        }

        List<Tile> DeepCopy()
        {
            var board = new List<Tile>(m_size * m_size);
            foreach (var tile in m_board)
                board.Add(new Tile { X = tile.X, Y = tile.Y, Value = tile.Value });
            return board;
        }

        IEnumerable<Tile> GetEmptyTiles()
        {
            return m_board.Where(x => x.Value == 0);
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

        private void Move(Func<Tile, int> predicate, bool up)
        {
            m_undoboards.Push(DeepCopy());
            StepsCount = StepsCount + 1;
            for (int c = 0; c < m_size; c++)
            {
                var col1 = new List<Tile>(m_board.Where(x => predicate(x) == c));

                for (int i = 0; i < m_size - 1; i++)
                    if (up)
                        MoveP(col1);
                    else
                        MoveN(col1);
            }
            NextFill();
        }

        private void MoveN(List<Tile> col1)
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

        private void MoveP(List<Tile> col1)
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

        #endregion Methods
    }
}
