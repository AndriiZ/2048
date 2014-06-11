using System;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino;
using _2048Lib;

namespace _2048Test
{
  [TestFixture]
  public class BoardTest
  {
     [Test]
     public void IsSingleMoveUpValid()
     {
         INextValueGenerator<sbyte> generatorMock = MockRepository.GenerateStub<INextValueGenerator<sbyte>>();
         generatorMock.Stub(m => m.Next(Arg<sbyte>.Is.Anything)).Return(4);//put initial value to cell "4" [1,0]
         generatorMock.Stub(m => m.Next()).Return(2); //cell Value shall be 2
         IBoard board = new Board(4, generatorMock);
         board.MoveUp(); //Now cell [0,0] must contains 2
         Assert.AreEqual(board.To2DArray()[0, 0].Value, 2);
     }
  }
}
