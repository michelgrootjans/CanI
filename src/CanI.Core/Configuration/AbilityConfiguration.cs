using System;
using CanI.Core.Authorization;

namespace CanI.Core.Configuration
{
    public static class AbilityConfiguration
    {
        private static Action<IAbilityConfiguration> configurationApplier;

        public static void ConfigureWith(Action<IAbilityConfiguration> configuration)
        {
            configurationApplier = configuration;
        }

        public static IAbility CreateAbility()
        {
            var ability = new Ability();
            if (configurationApplier == null) return ability;

            configurationApplier(ability);
            return ability;
        }

        public static void Reset()
        {
            configurationApplier = null;
        }
    }
}