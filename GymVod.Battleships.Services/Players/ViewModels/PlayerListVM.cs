using System;
using GymVod.Battleships.DataLayer.Model;

namespace GymVod.Battleships.Services.Players.ViewModels
{
    public class PlayerListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public League League { get; set; }
        public DateTime Created { get; set; }
    }
}
