namespace GymVod.Battleships.Common
{
    public class ShipPosition
    {
        public ShipType ShipType { get; }
        public Position Position { get; }
        public Orientation Orientation { get; }

        public ShipPosition(ShipType shipType, Position position, Orientation orientation)
        {
            ShipType = shipType;
            Position = position;
            Orientation = orientation;
        }
    }
}
