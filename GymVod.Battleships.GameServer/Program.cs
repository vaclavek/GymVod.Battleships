using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var di = new DirectoryInfo(@"c:\SVN\Jinsoft\GIT\GymVod\_Projekty\2019-2020\GymVod.Lode\GymVod.Lode.GameServer\Plugins\");
            var files = di.GetFiles("*.dll");

            var competitorTypes = new List<Type>();
            foreach (var file in files)
            {
                var a = Assembly.LoadFrom(file.FullName);
                var t = a.GetTypes();
                var u = t.Where(x => typeof(IBattleshipsGame).IsAssignableFrom(x));
                if (u.Count() == 1)
                {
                    competitorTypes.Add(u.Single());
                }
            }

            var competitors = new List<IBattleshipsGame>();
            foreach (var competitorType in competitorTypes)
            {
                var competitor = Activator.CreateInstance(competitorType);
                competitors.Add(competitor as IBattleshipsGame);
            }

            var settings = new GameSettings(15, 15, new ShipType[]
            {
                ShipType.Battleship
            });
            var games = new List<Game>();
            foreach (var competitor1 in competitors)
            {
                foreach (var competitor2 in competitors)
                {
                    if (competitor1 != competitor2)
                    {
                        games.Add(new Game
                        {
                            Player1 = competitor1,
                            Player2 = competitor2,
                            GameSettings = settings
                        });
                    }
                }
            }



        }



    }
}
