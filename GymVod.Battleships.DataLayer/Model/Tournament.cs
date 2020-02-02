using System;
using System.Collections.Generic;

namespace GymVod.Battleships.DataLayer.Model
{
    public class Tournament
    {
        public int Id { get; set; }
        public League League { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<Game> Games { get; } = new HashSet<Game>();
    }
}
