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

    public class StandardNextValueGenerator : INextValueGenerator<sbyte>
    {
	private readonly Random m_random = new Random();

	public sbyte Next()
	{
		return	(m_random.Next(100) % 10) != 0 ? (sbyte)2 : (sbyte)4;// 90% - 2, 10% - 4
	}

	public sbyte Next(sbyte limit)
	{
		return (sbyte)(m_random.Next(limit)); 
	}
    }
}

