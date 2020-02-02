using System.ComponentModel.DataAnnotations;

namespace GymVod.Battleships.DataLayer
{
    public enum WhichPlayerWins
    {
        NotFinished = 0,
        [Display(Name = "Hráč 1")]
        Player1,
        [Display(Name = "Hráč 2")]
        Player2,
        [Display(Name = "Remíza")]
        Draw
    }
}
