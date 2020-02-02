using GymVod.Battleships.Common;

namespace GymVod.Battleships.Services.GameServer
{
    public class Gameboard
    {
        private readonly byte boardWidth;
        private readonly byte boardHeight;

        private Ship[,] ShipsBoard { get; set; }

        public Gameboard(byte boardWidth, byte boardHeight)
        {
            ShipsBoard = new Ship[boardWidth, boardHeight];
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
        }

        public void PlaceShips(ShipPosition[] ships)
        {
            foreach (var ship in ships)
            {
                var newBoat = new Ship(ship.ShipType);

                var x = ship.Position.X;
                var y = ship.Position.Y;

                for (int i = 0; i < (int)ship.ShipType; i++)
                {
                    if (x == 0 || y == 0 || x == (boardWidth - 1) || y == (boardHeight - 1))
                    {
                        throw new GameOverException($"Nelze umístit loď {ship.ShipType} na pozici [{x},{y}], protože loď se nesmí dotýkat okraje herního pole.");
                    }

                    if (x < 0 || y > 0 || x >= boardWidth || y >= boardHeight)
                    {
                        throw new GameOverException($"Nelze umístit loď {ship.ShipType} na pozici [{x},{y}], protože pozice je mimo hrací pole.");
                    }

                    ShipsBoard[x, y] = newBoat;

                    switch (ship.Orientation)
                    {
                        case Orientation.Right:
                            x++;
                            break;
                        case Orientation.Down:
                            y++;
                            break;
                        case Orientation.Left:
                            x--;
                            break;
                        case Orientation.Up:
                            y--;
                            break;
                    }
                }
            }
        }

        public ShotResult Shoot(Position position)
        {
            if (position.X < 0 || position.Y < 0 || position.X >= boardWidth || position.Y >= boardHeight)
            {
                throw new GameOverException($"Střela [{position.X}, {position.Y}] je mimo hrací pole.");
            }

            var boat = ShipsBoard[position.X, position.Y];
            if (boat == null)
            {
                return new ShotResult(position, false, false);
            }

            var hit = boat.Shoot(position);
            return new ShotResult(position, hit, boat.ShipSunk);
        }

        public bool UserHasLost()
        {
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    var boat = ShipsBoard[x, y];
                    if (boat != null)
                    {
                        if (!boat.ShipSunk)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
