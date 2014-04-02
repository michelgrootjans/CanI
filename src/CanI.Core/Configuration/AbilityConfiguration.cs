using System;
using System.Collections.Generic;
using CanI.Core.Authorization;

namespace CanI.Core.Configuration
{
    public static class AbilityConfiguration
    {
        private static Action<IAbilityConfiguration> configurationApplier;
        private static ConfigurationLogger logger;
        private static ICache cache;

        static AbilityConfiguration()
        {
            Reset();
        }

        public static void ConfigureWith(Action<IAbilityConfiguration> configuration)
        {
            configurationApplier = configuration;
        }

        public static IVerbosityConfiguration Debug(Action<string> action)
        {
            return logger = new ConfigurationLogger(action);
        }

        public static IAbility GetAbility()
        {
            return cache.Get<IAbility>() ?? CreateAbility();
        }

        private static IAbility CreateAbility()
        {
            var ability = new Ability(logger);
            if (configurationApplier == null) return ability; //why this line? -- check later

            configurationApplier(ability);
            cache.Store<IAbility>(ability);
            return ability;
        }

        public static void Reset()
        {
            configurationApplier = (config) => { };
            logger = new ConfigurationLogger();
            cache = new NullCache();
        }

        public static void ConfigureCache(ICache c)
        {
            cache = c;
        }
    }

    public interface ICache
    {
        T Get<T>();
        void Store<T>(T item);
    }

    public interface IConfigurationLogger
    {
        void LogExecution(string message);
        void LogConfiguration(string message);
    }

    public interface IVerbosityConfiguration
    {
        void Verbose();
    }

    public class StaticCache : ICache
    {
        private readonly IDictionary<Type, Object> items = new Dictionary<Type, object>();

        public T Get<T>()
        {
            if (items.ContainsKey(typeof(T)))
                return (T) items[typeof (T)];
            return default(T);
        }

        public void Store<T>(T item)
        {
            items[typeof (T)] = item;
        }
    }

    public class NullCache : ICache
    {
        public T Get<T>() { return default(T); }
        public void Store<T>(T item) { }
    }

    public class ConfigurationLogger : IConfigurationLogger, IVerbosityConfiguration
    {
        private readonly Action<string> logAction;
        private bool logVerbose;

        public ConfigurationLogger(Action<string> logAction)
        {
            this.logAction = logAction;
        }

        public ConfigurationLogger()
            : this(t => { })
        {
        }

        public void Verbose()
        {
            logVerbose = true;
        }

        public void LogExecution(string message)
        {
            logAction(message);
        }

        public void LogConfiguration(string message)
        {
            if (logVerbose) logAction(message);
        }
    }
}