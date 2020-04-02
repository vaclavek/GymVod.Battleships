using System;
using System.IO;
using System.Linq;
using System.Reflection;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.Services.Players
{
    public class PluginLoader : IPluginLoader
    {
        public bool TestImplementation(Assembly assembly)
        {
            var implementationType = GetImplementationType(assembly);
            if (implementationType == null)
            {
                throw new Exception("Knihovna musí mít právě jednu třídu, která implementuje " + nameof(IBattleshipsGame));
            }

            return TryCreateInstance(implementationType) != null;
        }

        public Assembly LoadPlugin(Guid fileGuid)
        {
            var path = Path.Combine("Upload", $"{fileGuid}.dll");
            return Assembly.LoadFrom(path);
        }

        public IBattleshipsGame GetInstance(Assembly assembly)
        {
            var type = GetImplementationType(assembly);
            return TryCreateInstance(type);
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

        private IBattleshipsGame TryCreateInstance(Type type)
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

            return game;
        }
    }
}
