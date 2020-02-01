namespace GymVod.Battleships.Common
{
    public class GameSettings
    {
        public byte BoardWidth { get; }
        public byte BoardHeight { get; }
        public ShipType[] ShipTypes { get; }

        public GameSettings(byte boardWidth, byte boardHeight, ShipType[] shipTypes)
        {
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            ShipTypes = shipTypes;
        }
    }
}
