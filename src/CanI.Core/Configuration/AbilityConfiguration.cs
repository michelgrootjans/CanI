using System;
using CanI.Core.Authorization;

namespace CanI.Core.Configuration
{
    public static class AbilityConfiguration
    {
        private static Action<IAbilityConfiguration> configurationApplier;
        private static ConfigurationLogger logger;

        public static void ConfigureWith(Action<IAbilityConfiguration> configuration)
        {
            configurationApplier = configuration;
        }

        public static IVerbosityConfiguration Debug(Action<string> action)
        {
            return logger = new ConfigurationLogger(action);
        }

        public static IAbility CreateAbility()
        {
            var ability = new Ability(logger);
            if (configurationApplier == null) return ability;

            configurationApplier(ability);
            return ability;
        }

        public static void Reset()
        {
            configurationApplier = null;
            logger = new ConfigurationLogger();
        }
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
            if(logVerbose) logAction(message);
        }
    }
}