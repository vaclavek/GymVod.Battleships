using System;

namespace GymVod.Battleships.Common
{
    public sealed class Position : IEquatable<Position>
    {
        public byte X { get; }
        public byte Y { get; }

        public Position(byte x, byte y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Position other)
        {
            if (other == null)
            {
                return false;
            }

            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Position positionObj = obj as Position;
            if (positionObj == null)
            {
                return false;
            }
            else
            {
                return Equals(positionObj);
            }
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public static bool operator ==(Position position1, Position position2)
        {
            if (((object)position1) == null || ((object)position2) == null)
                return Equals(position1, position2);

            return position1.Equals(position2);
        }

        public static bool operator !=(Position position1, Position position2)
        {
            if (((object)position1) == null || ((object)position2) == null)
                return !Equals(position1, position2);

            return !(position1.Equals(position2));
        }
    }
}
