namespace GymVod.Battleships.Common
{
    public class ShotResult
    {
        public Position Position { get; }
        public bool Hit { get; }
        public bool ShipSunken { get; }

        public ShotResult(Position position, bool hit, bool shipSunken)
        {
            Position = position;
            Hit = hit;
            ShipSunken = shipSunken;
        }
    }
}
