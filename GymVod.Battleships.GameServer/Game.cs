using GymVod.Battleships.Common;

namespace GymVod.Battleships.GameServer
{
    public class Game
    {
        public IBattleshipsGame Player1 { get; set; }
        public IBattleshipsGame Player2 { get; set; }
        public GameSettings GameSettings { get; set; }
        public int RoundsCount { get; set; }
        public ShipPosition[] Position1 { get; set; }
        public ShipPosition[] Position2 { get; set; }
        public bool Player1OnTurn { get; set; }
        public bool Player1Won { get; set; }
        public bool Player2Won { get; set; }
    }
}
