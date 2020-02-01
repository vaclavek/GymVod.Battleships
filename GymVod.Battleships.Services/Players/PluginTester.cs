using System;
using System.Linq;
using System.Reflection;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.Services.Players
{
    public class PluginTester : IPluginTester
    {
        public bool TestImplementation(Assembly assembly)
        {
            var implementationType = GetImplementationType(assembly);
            if (implementationType == null)
            {
                throw new Exception("Knihovna musí mít právě jednu třídu, která implementuje " + nameof(IBattleshipsGame));
            }

            return CanCreateInstance(implementationType);
        }

        private Type GetImplementationType(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var assignableTypes = types.Where(x => typeof(IBattleshipsGame).IsAssignableFrom(x));

            if (assignableTypes.Count() == 1)
            {
                return assignableTypes.Single();
            }
            return null;
        }

        private bool CanCreateInstance(Type type)
        {
            IBattleshipsGame game;
            try
            {
                var competitor = Activator.CreateInstance(type);
                game = competitor as IBattleshipsGame;
            }
            catch (Exception exception)
            {
                // it is not possible to create instance
                throw new Exception("Nepodařilo se vytvořit instanci (má třída bezparametrický konstruktor?).", exception);
            }

            if (game == null)
            {
                throw new Exception("Nepodařilo se použít třídu jako rozhraní");
            }

            return true;
        }
    }
}
