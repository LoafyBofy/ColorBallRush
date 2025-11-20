using System;
using System.Collections.Generic;

public static class ServiceLocator 
{
    private static Dictionary<Type, object> _services = new();

    public static void RegisterService<T>(T service) where T : class
    {
        _services.Add(typeof(T), service);
    }

    public static void UnregisterService<T>(T service) where T : class
    {
        var type = typeof(T);

        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
    }

    public static T GetService<T>(T serviceType) where T : class
    {
        _services.TryGetValue(typeof(T), out var service);

        return service as T;
    }

    public static void UnregisterAll()
    {
        _services.Clear();
    }
}
