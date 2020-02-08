using System;
using System.Collections.Generic;
using System.Linq;
using GymVod.Battleships.Common;
using GymVod.Battleships.DataLayer;

namespace GymVod.Battleships.Services.GameServer
{
    public class GameEngine
    {
        private readonly Game game;
        private readonly Gameboard gameboard1;
        private readonly Gameboard gameboard2;

        public GameEngine(Game game)
        {
            this.game = game;
            gameboard1 = new Gameboard(game.GameSettings.BoardWidth, game.GameSettings.BoardHeight);
            gameboard2 = new Gameboard(game.GameSettings.BoardWidth, game.GameSettings.BoardHeight);
        }

        public void InitGame()
        {
            ShipPosition[] shipPositions;
            try
            {
                shipPositions = game.Player1.NewGame(game.GameSettings);
            }
            catch (Exception e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player2;
                game.ErrorMessage = $"V metodě {nameof(IBattleshipsGame.NewGame)} vznikla neošetřená výjimka: {e.Message}";
                return;
            }

            if (!PlacedAllShips(game.GameSettings, shipPositions))
            {
                game.WhichPlayerWins = WhichPlayerWins.Player2;
                game.ErrorMessage = $"Hráč neumístil všechny lodě.";
                return;
            }

            try
            {
                gameboard1.PlaceShips(shipPositions);
            }
            catch (GameOverException e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player2;
                game.ErrorMessage = e.Message;
                return;
            }

            try
            {
                shipPositions = game.Player2.NewGame(game.GameSettings);
            }
            catch (Exception e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player1;
                game.ErrorMessage = $"V metodě {nameof(IBattleshipsGame.NewGame)} vznikla neošetřená výjimka: {e.Message}";
                return;
            }

            if (!PlacedAllShips(game.GameSettings, shipPositions))
            {
                game.WhichPlayerWins = WhichPlayerWins.Player1;
                game.ErrorMessage = $"Hráč neumístil všechny lodě.";
                return;
            }

            try
            {
                gameboard2.PlaceShips(shipPositions);
            }
            catch (GameOverException e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player1;
                game.ErrorMessage = e.Message;
                return;
            }
        }

        public void PlayGame()
        {
            // dvojnásobná rezerva pro "náhodné střelce"
            var roundsCount = game.GameSettings.BoardWidth * game.GameSettings.BoardHeight * 2;
            for (int i = 1; i <= roundsCount; i++)
            {
                game.RoundsCount = i;

                PlayRound();

                if (gameboard1.UserHasLost() || game.WhichPlayerWins == WhichPlayerWins.Player2)
                {
                    game.WhichPlayerWins = WhichPlayerWins.Player2;
                    return;
                }

                if (gameboard2.UserHasLost() || game.WhichPlayerWins == WhichPlayerWins.Player1)
                {
                    game.WhichPlayerWins = WhichPlayerWins.Player1;
                    return;
                }
            }

            game.WhichPlayerWins = WhichPlayerWins.Draw;
        }

        private void PlayRound()
        {
            Position shotPosition;
            try
            {
                shotPosition = game.Player1.GetNextShotPosition();
            }
            catch (Exception e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player2;
                game.ErrorMessage = $"V metodě {nameof(IBattleshipsGame.GetNextShotPosition)} vznikla neošetřená výjimka: {e.Message}";
                return;
            }

            ShotResult result;
            try
            {
                result = gameboard2.Shoot(shotPosition);
            }
            catch (GameOverException e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player2;
                game.ErrorMessage = e.Message;
                return;
            }

            try
            {
                game.Player1.ShotResult(result);
            }
            catch (Exception e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player2;
                game.ErrorMessage = $"V metodě {nameof(IBattleshipsGame.ShotResult)} vznikla neošetřená výjimka: {e.Message}";
                return;
            }

            if (!gameboard2.UserHasLost())
            {
                try
                {
                    shotPosition = game.Player2.GetNextShotPosition();
                }
                catch (Exception e)
                {
                    game.WhichPlayerWins = WhichPlayerWins.Player1;
                    game.ErrorMessage = $"V metodě {nameof(IBattleshipsGame.GetNextShotPosition)} vznikla neošetřená výjimka: {e.Message}";
                    return;
                }

                try
                {
                    result = gameboard1.Shoot(shotPosition);
                }
                catch (GameOverException e)
                {
                    game.WhichPlayerWins = WhichPlayerWins.Player1;
                    game.ErrorMessage = e.Message;
                    return;
                }

                try
                {
                    game.Player2.ShotResult(result);
                }
                catch (Exception e)
                {
                    game.WhichPlayerWins = WhichPlayerWins.Player1;
                    game.ErrorMessage = $"V metodě {nameof(IBattleshipsGame.ShotResult)} vznikla neošetřená výjimka: {e.Message}";
                    return;
                }

            }
        }

        private bool PlacedAllShips(GameSettings gameSettings, ShipPosition[] shipPositions)
        {
            var tempPositions = new List<ShipPosition>();
            tempPositions.AddRange(shipPositions);

            foreach (var shipType in gameSettings.ShipTypes)
            {
                var position = tempPositions.FirstOrDefault(x => x.ShipType == shipType);
                if (position == null)
                {
                    return false;
                }

                tempPositions.Remove(position);
            }

            return true;
        }
    }
}
