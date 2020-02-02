using GymVod.Battleships.Common;

namespace GymVod.Battleships.GameServer
{
    public class Game
    {
        public GameSettings GameSettings { get; set; }

        public IBattleshipsGame Player1 { get; set; }
        public IBattleshipsGame Player2 { get; set; }

        public int RoundsCount { get; set; }

        public WhichPlayerWins WhichPlayerWins { get; set; }
        public string EndMessage { get; set; }
    }
}
