using System;

namespace CanI.Core
{
    public static class I
    {
        public static bool Can(string action, object subject)
        {
            var ability = AbilityConfiguration.CreateAbility();
            return ability.Allows(action, subject);
        }
    }

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

        internal static void Reset()
        {
            configurationApplier = null;
        }
    }
}