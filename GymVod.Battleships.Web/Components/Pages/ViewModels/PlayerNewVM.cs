using System;
using System.ComponentModel.DataAnnotations;

namespace GymVod.Battleships.Web.Components.Pages.ViewModels
{
    public class PlayerNewVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [CompareProperty(nameof(Password))]
        public string PasswordCompare { get; set; }

        [Required]
        public string League { get; set; }

        public Guid FileGuid { get; } = Guid.NewGuid();
    }
}
