namespace GymVod.Battleships.Common
{
    public sealed class Position
    {
        public byte X { get; }
        public byte Y { get; }

        public Position(byte x, byte y)
        {
            X = x;
            Y = y;
        }
    }
}
