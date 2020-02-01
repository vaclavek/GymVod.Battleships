using System.Reflection;

namespace GymVod.Battleships.Services.Players
{
    public interface IPluginTester
    {
        bool TestImplementation(Assembly assembly);
    }
}