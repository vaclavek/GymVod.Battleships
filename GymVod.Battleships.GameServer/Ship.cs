using System.Collections.Generic;
using System.Linq;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.GameServer
{
    public class Ship
    {
        public ShipType ShipType { get; }
        public bool ShipSunk => Hits.Count == (int)ShipType;

        private List<Position> Hits { get; } = new List<Position>();

        public Ship(ShipType shipType)
        {
            ShipType = shipType;
        }

        public bool Shoot(Position position)
        {
            if (!Hits.Any(x => x == position))
            {
                Hits.Add(position);
                return true;
            }

            return false;
        }
    }
}
