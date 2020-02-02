using System;

namespace GymVod.Battleships.Services.GameServer
{
    public class GameOverException : ApplicationException
    {
        public GameOverException(string mesage) : base(mesage)
        {
        }
    }
}
