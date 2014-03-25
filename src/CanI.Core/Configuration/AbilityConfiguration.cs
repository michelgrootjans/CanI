using System;
using CanI.Core.Authorization;

namespace CanI.Core.Configuration
{
    public static class AbilityConfiguration
    {
        private static Action<IAbilityConfiguration> configurationApplier;
        private static Action<string> debugAction = t => {};
        private static VerbosityConfiguration verbosity;

        public static void ConfigureWith(Action<IAbilityConfiguration> configuration)
        {
            configurationApplier = configuration;
        }

        public static IVerbosityConfiguration Debug(Action<string> action)
        {
            debugAction = action;
            verbosity = new VerbosityConfiguration();
            return verbosity;
        }

        public static IAbility CreateAbility()
        {
            var ability = new Ability(debugAction, verbosity.IsVerbose());
            if (configurationApplier == null) return ability;

            configurationApplier(ability);
            return ability;
        }

        public static void Reset()
        {
            configurationApplier = null;
            debugAction = t => { };
        }
    }

    public class VerbosityConfiguration : IVerbosityConfiguration
    {
        private bool verbosity;

        public void Verbose()
        {
            verbosity = true;
        }

        public bool IsVerbose()
        {
            return verbosity;
        }
    }

    public interface IVerbosityConfiguration
    {
        void Verbose();
    }
}