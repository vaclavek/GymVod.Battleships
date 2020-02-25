using GymVod.Battleships.Common;
using GymVod.Battleships.Services.GameServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GymVod.Battleships.Tests
{
    [TestClass]
    public class GameboardPlaceTests
    {
        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotBePlacedOutsideOfBoardInRight()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(20,2), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotBePlacedOutsideOfBoardInBottom()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(3,20), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotTouchBoardInTop()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Submarine, new Position(3,0), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotTouchBoardInRight()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Submarine, new Position(9,3), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotTouchBoardInBottom()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Submarine, new Position(3,9), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotTouchBoardInLeft()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Submarine, new Position(0,5), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotOverlap1()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(1,1), Orientation.Down),
                new ShipPosition(ShipType.Carrier, new Position(1,1), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotOverlap2()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(1,1), Orientation.Down),
                new ShipPosition(ShipType.Cruiser, new Position(1,4), Orientation.Up)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotOverlap3()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(1,1), Orientation.Right),
                new ShipPosition(ShipType.Cruiser, new Position(4,1), Orientation.Left)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotTouchEachOtherRight()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(1,1), Orientation.Right),
                new ShipPosition(ShipType.Cruiser, new Position(5,1), Orientation.Left)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void GameboardTest_ShipsCannotTouchEachOtherDown()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(1,1), Orientation.Down),
                new ShipPosition(ShipType.Cruiser, new Position(1,5), Orientation.Up)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);

            // assert (exception is thrown)
        }

    }
}
