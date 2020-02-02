using System;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.Services.GameServer
{
    public class GameEngine : IGameEngine
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
            try
            {
                gameboard1.PlaceShips(game.Player1.NewGame(game.GameSettings));
            }
            catch (GameOverException e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player2;
                game.EndMessage = e.Message;
                return;
            }

            try
            {
                gameboard2.PlaceShips(game.Player2.NewGame(game.GameSettings));
            }
            catch (GameOverException e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player1;
                game.EndMessage = e.Message;
                return;
            }

            PlayGame();
        }

        private void PlayGame()
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
                game.EndMessage = $"V metodě {nameof(IBattleshipsGame.GetNextShotPosition)} vznikla neošetřená výjimka: {e.Message}";
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
                game.EndMessage = e.Message;
                return;
            }

            try
            {
                game.Player1.ShotResult(result);
            }
            catch (Exception e)
            {
                game.WhichPlayerWins = WhichPlayerWins.Player2;
                game.EndMessage = $"V metodě {nameof(IBattleshipsGame.ShotResult)} vznikla neošetřená výjimka: {e.Message}";
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
                    game.EndMessage = $"V metodě {nameof(IBattleshipsGame.GetNextShotPosition)} vznikla neošetřená výjimka: {e.Message}";
                    return;
                }

                try
                {
                    result = gameboard1.Shoot(shotPosition);
                }
                catch (GameOverException e)
                {
                    game.WhichPlayerWins = WhichPlayerWins.Player1;
                    game.EndMessage = e.Message;
                    return;
                }

                try
                {
                    game.Player2.ShotResult(result);
                }
                catch (Exception e)
                {
                    game.WhichPlayerWins = WhichPlayerWins.Player1;
                    game.EndMessage = $"V metodě {nameof(IBattleshipsGame.ShotResult)} vznikla neošetřená výjimka: {e.Message}";
                    return;
                }

            }
        }
    }
}
