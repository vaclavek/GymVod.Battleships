using System;
using System.Reflection;
using System.Runtime.Loader;
using GymVod.Battleships.Common;

namespace GymVod.Battleships.Services.Players
{
    public interface IPluginLoader
    {
        bool TestImplementation(Assembly assembly);
        Assembly LoadPlugin(AssemblyLoadContext assemblyLoadContext, Guid fileGuid);
        IBattleshipsGame GetInstance(Assembly assembly);
    }
}