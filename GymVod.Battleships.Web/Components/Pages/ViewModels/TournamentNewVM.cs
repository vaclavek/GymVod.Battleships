using System.ComponentModel.DataAnnotations;

namespace GymVod.Battleships.Web.Components.Pages.ViewModels
{
    public class TournamentNewVM
    {
        [Required]
        public string League { get; set; }
    }
}
