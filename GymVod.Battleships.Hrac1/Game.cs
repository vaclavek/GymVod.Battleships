﻿using GymVod.Battleships.Common;

namespace GymVod.Battleships.Hrac1
{
    public class Game : IBattleshipsGame
    {
        public ShipPosition[] NewGame(GameSettings nastaveniHry)
        {
            return new ShipPosition[]
            {
                new ShipPosition(ShipType.BitevniLod, new Position(0,0), Orientation.Right)
            };
        }

        public Position GetNextShotPosition()
        {
            return new Position(0, 0);
        }

        public void ShotInformation(Position souradnice, bool zasah, bool potopena)
        {
        }
    }
}
