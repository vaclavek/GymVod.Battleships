using System;

namespace GymVod.Battleships.DataLayer.Model
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public League League { get; set; }
        public Guid FileGuid { get; set; }
        public DateTime Created { get; set; }
        public string IP { get; set; }
    }
}
