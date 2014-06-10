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

	public interface IGameEngine
	{
		bool IsAI();
		NextStepCommand GetNextStep(ITile[,] board);
	}
	
	public interface ITile
	{
		int Value { get; }
		SByte X { get; }
		SByte Y { get; }
	}

	public interface IBoard
	{
        	int Score { get; }
	        int StepsCount { get; }
		bool NextStepAvailable();
		ITile[,] To2DArray();
	        void MoveUp();
        	void MoveDown();
	        void MoveLeft();
        	void MoveRight();
	}
	
	public interface IStatefullBoard : IBoard
	{
		void Undo();	
	}
}

