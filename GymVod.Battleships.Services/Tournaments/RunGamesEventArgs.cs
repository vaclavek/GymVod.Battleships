using System;

namespace GymVod.Battleships.Services.Tournaments
{
    public class RunGamesEventArgs : EventArgs
    {
        public int GamesRunCount { get; set; }
        public int GamesTotalCount { get; set; }
    }
}
