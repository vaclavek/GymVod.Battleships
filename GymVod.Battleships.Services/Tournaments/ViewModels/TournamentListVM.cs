﻿using System;
using System.Collections.Generic;
using GymVod.Battleships.DataLayer.Model;

namespace GymVod.Battleships.Services.Tournaments.ViewModels
{
    public class TournamentListVM
    {
        public int Id { get; set; }
        public League League { get; set; }
        public DateTime Created { get; set; }
        public int GamesCount { get; set; }
        public List<TournamentListPlayerVM> Players { get; set; }

    }
}
