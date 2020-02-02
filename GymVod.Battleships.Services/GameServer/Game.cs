using GymVod.Battleships.Common;
using GymVod.Battleships.DataLayer;

namespace GymVod.Battleships.Services.GameServer
{
    public class Game
    {
        public GameSettings GameSettings { get; set; }

        public IBattleshipsGame Player1 { get; set; }
        public IBattleshipsGame Player2 { get; set; }

        public int Player1Id { get; set; }
        public int Player2Id { get; set; }

        public int RoundsCount { get; set; }

        public WhichPlayerWins WhichPlayerWins { get; set; } = WhichPlayerWins.NotFinished;
        public string ErrorMessage { get; set; }
    }
}
