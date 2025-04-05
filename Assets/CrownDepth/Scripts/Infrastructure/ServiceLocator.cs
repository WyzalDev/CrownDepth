using System;
using System.Collections.Generic;

namespace CrownDepth.Infrastructure
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new();
        
        public void Register<T>(T service)
        {
            _services[typeof(T)] = service;
        }

        public bool Unregister<T>()
        {
            return _services.Remove(typeof(T));
        }

        public T Get<T>()
        {
            return (T)_services[typeof(T)];
        }

        public bool IsRegistered<T>()
        {
            return _services.ContainsKey(typeof(T));
        }
    }
}