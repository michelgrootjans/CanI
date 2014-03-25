using System;
using CanI.Core.Authorization;

namespace CanI.Core.Configuration
{
    public static class AbilityConfiguration
    {
        private static Action<IAbilityConfiguration> configurationApplier;
        private static Action<string> debugAction = t => {};

        public static void ConfigureWith(Action<IAbilityConfiguration> configuration)
        {
            configurationApplier = configuration;
        }

        public static IAbility CreateAbility()
        {
            var ability = new Ability(debugAction);
            if (configurationApplier == null) return ability;

            configurationApplier(ability);
            return ability;
        }

        public static void Reset()
        {
            configurationApplier = null;
        }

        public static void Debug(Action<string> action)
        {
            debugAction = action;
        }
    }
}