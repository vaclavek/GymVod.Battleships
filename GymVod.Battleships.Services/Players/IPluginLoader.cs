using System;
using System.Reflection;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.Services.Players
{
    public interface IPluginLoader
    {
        bool TestImplementation(Assembly assembly);
        Assembly LoadPlugin(Guid fileGuid);
        IBattleshipsGame GetInstance(Assembly assembly);
    }
}