namespace GymVod.Battleships.Common
{
    public class ShipPosition
    {
        public ShipType ShipType { get; }
        public Position Souradnice { get; }
        public Orientation Orientace { get; }

        public ShipPosition(ShipType typLode, Position souradnice, Orientation orientace)
        {
            ShipType = typLode;
            Souradnice = souradnice;
            Orientace = orientace;
        }
    }
}
