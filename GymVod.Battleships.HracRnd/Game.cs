using System;
using System.Collections.Generic;
using System.Linq;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.HracRnd
{
    public class Game : IBattleshipsGame
    {
        private byte width;
        private byte height;

        public ShipPosition[] NewGame(GameSettings nastaveniHry)
        {
            width = nastaveniHry.BoardWidth;
            height = nastaveniHry.BoardHeight;

            var shipPositions = new List<ShipPosition>();
            byte y = 1;
            foreach (var shipType in nastaveniHry.ShipTypes.OrderByDescending(x => (int)x))
            {
                var position = new Position(1, y);
                shipPositions.Add(new ShipPosition(shipType, position, Orientation.Right));
                y += 2;
            }

            return shipPositions.ToArray();
        }

        public Position GetNextShotPosition()
        {
            var rnd = new Random();
            var x = (byte)rnd.Next(1, width - 1);
            var y = (byte)rnd.Next(1, height - 1);

            return new Position(x, y);
        }

        public void ShotResult(ShotResult shotResult)
        {
        }
    }
}
