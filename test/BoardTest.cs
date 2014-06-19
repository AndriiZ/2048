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
      public IGameBoard InitBoard(sbyte size, INextValueGenerator<sbyte> generator)
      {
          return new Board(size, generator);
      }

     [Test]
     public void IsSingleMoveUpValid()
     {
         INextValueGenerator<sbyte> generatorMock = MockRepository.GenerateStub<INextValueGenerator<sbyte>>();
         generatorMock.Stub(m => m.NextPosition(Arg<sbyte>.Is.Anything)).Return(4);//put initial value to cell "4" [1,0]
         generatorMock.Stub(m => m.NextValue()).Return(2); //cell Value shall be 2
         IGameBoard board = InitBoard(4, generatorMock);
         board.MoveUp(); //Now cell [0,0] must contains 2
         Assert.AreEqual(board.To2DArray()[0, 0].Value, 2);
     }

     [Test]
     public void IsSingleMoveDownValid()
     {
         INextValueGenerator<sbyte> generatorMock = MockRepository.GenerateStub<INextValueGenerator<sbyte>>();
         generatorMock.Stub(m => m.NextPosition(Arg<sbyte>.Is.Anything)).Return(0);//put initial value to cell "0" [0,0]
         generatorMock.Stub(m => m.NextValue()).Return(2); //cell Value shall be 2
         IGameBoard board = InitBoard(4, generatorMock);
         board.MoveDown(); //Now cell [3,0] must contains 2
         Assert.AreEqual(board.To2DArray()[board.Size-1, 0].Value, 2);
     }

     [Test]
     public void IsSingleMoveLeftValid()
     {
         INextValueGenerator<sbyte> generatorMock = MockRepository.GenerateStub<INextValueGenerator<sbyte>>();
         generatorMock.Stub(m => m.NextPosition(Arg<sbyte>.Is.Anything)).Return(1);//put initial value to cell "1" [0,1]
         generatorMock.Stub(m => m.NextValue()).Return(2); //cell Value shall be 2
         IGameBoard board = InitBoard(4, generatorMock);
         board.MoveLeft(); //Now cell [0,0] must contains 2
         Assert.AreEqual(board.To2DArray()[0, 0].Value, 2);
     }

     [Test]
     public void IsSingleMoveRightValid()
     {
         INextValueGenerator<sbyte> generatorMock = MockRepository.GenerateStub<INextValueGenerator<sbyte>>();
         generatorMock.Stub(m => m.NextPosition(Arg<sbyte>.Is.Anything)).Return(0);//put initial value to cell "4" [0,0]
         generatorMock.Stub(m => m.NextValue()).Return(2); //cell Value shall be 2
         IGameBoard board = InitBoard(4, generatorMock);
         board.MoveRight(); //Now cell [0,1] must contains 2
         Assert.AreEqual(board.To2DArray()[0, board.Size - 1].Value, 2);
     }

  }
}
