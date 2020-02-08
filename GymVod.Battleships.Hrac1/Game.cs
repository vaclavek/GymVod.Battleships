using System.Collections.Generic;
using System.Linq;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.Hrac1
{
    public class Game : IBattleshipsGame
    {
        public ShipPosition[] NewGame(GameSettings nastaveniHry)
        {
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
            return new Position(1, 1);
        }

        public void ShotResult(ShotResult shotResult)
        {
        }
    }
}
