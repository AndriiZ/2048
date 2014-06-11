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

namespace _2048Lib
{
    using System;

    public class Game2048 : IGame
    {
        #region Fields

        IStatefullBoard m_board;
        IGameEngine m_engine;
        IGameOptions m_options;

        #endregion Fields

        #region Constructors

        public Game2048(IGameEngine engine, IGameOptions options, IStatefullBoard board)
        {
            m_engine = engine;
            m_options = options;
            m_board = board;
        }

        #endregion Constructors

        #region Events

        public event RepaintRequiredEventHandler RepaintRequiredEvent;

        #endregion Events

        #region Methods

        public void Play()
        {
            bool running = true;
            while (running)
            {
                NextStepCommand command = m_engine.GetNextStep(m_board.To2DArray());

                switch (command)
                {
                    case NextStepCommand.Up :
                        m_board.MoveUp();
                        break;
                    case NextStepCommand.Down:
                        m_board.MoveDown();
                        break;
                    case NextStepCommand.Right:
                        m_board.MoveRight();
                        break;
                    case NextStepCommand.Left:
                        m_board.MoveLeft();
                        break;
                    case NextStepCommand.Undo:
                        if (!m_engine.IsAI())
                m_board.Undo();
                        break;
                    case NextStepCommand.Break:
                        running = false;
                        break;
                }

                if (running)
                    OnRepaintRequired(new  BoardEventArgs { Board = m_board });

                if (!m_board.NextStepAvailable())
                {
                    running = false;
                }
                if (running && m_engine.IsAI() && m_board.StepsCount > m_options.MaxStepCount)
                {
                    running = false;
                }
            }
        }

        protected virtual void OnRepaintRequired(BoardEventArgs e)
        {
            if (RepaintRequiredEvent != null)
                RepaintRequiredEvent(this, e);
        }

        #endregion Methods
    }
}
