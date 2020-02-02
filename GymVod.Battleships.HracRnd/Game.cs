using System;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.HracRnd
{
    public class Game : IBattleshipsGame
    {
        public ShipPosition[] NewGame(GameSettings nastaveniHry)
        {
            var rnd = new Random();
            var x = (byte)rnd.Next(0, nastaveniHry.BoardWidth);
            var y = (byte)rnd.Next(0, nastaveniHry.BoardHeight);

            return new ShipPosition[]
            {
                new ShipPosition(ShipType.Battleship, new Position(x, y), Orientation.Right)
            };
        }

        public Position GetNextShotPosition()
        {
            var rnd = new Random();
            var x = (byte)rnd.Next(0, 15);
            var y = (byte)rnd.Next(0, 15);

            return new Position(x, y);
        }

        public void ShotResult(ShotResult shotResult)
        {
        }
    }
}
