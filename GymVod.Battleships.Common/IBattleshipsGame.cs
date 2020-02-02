namespace GymVod.Battleships.Common
{
    public interface IBattleshipsGame
    {
        /// <summary>
        /// Vygenerování nové hry - rozmístění lodí
        /// </summary>
        ShipPosition[] NewGame(GameSettings gameSettings);

        /// <summary>
        /// Vrať místo, kam chcete provést další střelu
        /// </summary>
        Position GetNextShotPosition();

        /// <summary>
        /// Informace o zásahu po výstřelu
        /// </summary>
        void ShotResult(ShotResult shotResult);
    }
}
