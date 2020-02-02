using GymVod.Battleships.DataLayer;

namespace GymVod.Battleships.Services.Tournaments.ViewModels
{
    public class TournamentGameListVM
    {
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public int RoundsCount { get; set; }
        public WhichPlayerWins WhichPlayerWins { get; set; }
        public string ErrorMessage { get; set; }
    }
}
