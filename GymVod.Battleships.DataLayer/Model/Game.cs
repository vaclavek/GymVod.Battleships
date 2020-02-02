using System;

namespace GymVod.Battleships.DataLayer.Model
{
    public class Game
    {
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public Player Player1 { get; set; }

        public int Player2Id { get; set; }
        public Player Player2 { get; set; }

        public int RoundsCount { get; set; }
        public WhichPlayerWins WhichPlayerWins { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
