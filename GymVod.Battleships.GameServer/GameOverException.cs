using System;

namespace GymVod.Battleships.GameServer
{
    public class GameOverException : ApplicationException
    {
        public GameOverException(string mesage) : base(mesage)
        {
        }
    }
}
