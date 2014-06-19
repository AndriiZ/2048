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
    using System.Collections.Generic;

    #region Enumerations

    public enum NextStepCommand
    {
        Up,
        Down,
        Right,
        Left,
        Break,
        Undo,
        Nop
    }

    #endregion Enumerations

    #region Delegates

    public delegate void RepaintRequiredEventHandler(object sender, BoardEventArgs e);

    #endregion Delegates

    public interface IBoard
    {
        int Score
        {
            get;
        }

        int StepsCount
        {
            get;
        }

        SByte Size
        {
            get;
        }

        ITile[,] To2DArray();
    }

    public interface IGameBoard : IBoard
    {
        #region Properties

	    INextValueGenerator<sbyte> NextValueGenerator { get; }

        #endregion Properties

        #region Methods

        void MoveDown();

        void MoveLeft();

        void MoveRight();

        void MoveUp();

        bool NextStepAvailable();

        #endregion Methods
    }

    public interface IGame
    {
        #region Events

        event RepaintRequiredEventHandler RepaintRequiredEvent;

        #endregion Events

        #region Methods

        void Play();

        #endregion Methods
    }

    public interface IGameEngine
    {
        #region Methods

        NextStepCommand GetNextStep(ITile[,] board);

        bool IsAI();

        #endregion Methods
    }

    public interface IGameOptions
    {
        #region Properties

        int MaxStepCount
        {
            get;
        }

        #endregion Properties
    }

    public interface IStatefullBoard : IGameBoard
    {
        #region Methods

        void Undo();

        #endregion Methods
    }

    public interface ITile
    {
        #region Properties

        int Value
        {
            get;
        }

        SByte X
        {
            get;
        }

        SByte Y
        {
            get;
        }

        #endregion Properties
    }

    public class BoardEventArgs : EventArgs
    {
        #region Properties

        public IBoard Board
        {
            get; set;
        }

        #endregion Properties
    }

   public interface INextValueGenerator<T>
   {
	    T NextValue();
	    T NextPosition(T limit);
   }
}
