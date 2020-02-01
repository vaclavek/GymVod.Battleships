using System;
using GymVod.Battleships.DataLayer.Model;

namespace GymVod.Battleships.Services.Players.ViewModels
{
    public class PlayerVM
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public League League { get; set; }
        public Guid FileGuid { get; set; }
        public DateTime Created { get; } = DateTime.Now;
    }
}
