using System.ComponentModel.DataAnnotations;

namespace GymVod.Battleships.Web.Model
{
    public class UploadNewGameModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
