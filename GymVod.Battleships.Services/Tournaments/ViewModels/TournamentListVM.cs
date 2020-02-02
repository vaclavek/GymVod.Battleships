using System;
using System.Collections.Generic;
using GymVod.Battleships.DataLayer.Model;

namespace GymVod.Battleships.Services.Tournaments.ViewModels
{
    public class TournamentListVM
    {
        public League League { get; set; }
        public DateTime Created { get; set; }
        public List<TournamentGameListVM> Games { get; set; }
    }
}
