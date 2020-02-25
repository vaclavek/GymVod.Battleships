using GymVod.Battleships.Common;
using GymVod.Battleships.Services.GameServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GymVod.Battleships.Tests
{
    [TestClass]
    public class ShootTests
    {

        [TestMethod]
        public void GameboardTest_DestroyerSunk()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(1,1), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);
            var result1 = gb1.Shoot(new Position(1, 1));
            var result2 = gb1.Shoot(new Position(1, 2));

            // assert
            Assert.IsTrue(result1.Hit);
            Assert.IsFalse(result1.ShipSunken);

            Assert.IsTrue(result2.Hit);
            Assert.IsTrue(result2.ShipSunken);
        }

        [TestMethod]
        public void GameboardTest_UserHasLost()
        {
            // arrange
            ShipPosition[] s1 =
            {
                new ShipPosition(ShipType.Destroyer, new Position(1,1), Orientation.Down)
            };

            Gameboard gb1 = new Gameboard(10, 10);

            // act
            gb1.PlaceShips(s1);
            gb1.Shoot(new Position(1, 1));
            gb1.Shoot(new Position(1, 2));

            // assert
            Assert.IsTrue(gb1.UserHasLost());
        }

    }
}
