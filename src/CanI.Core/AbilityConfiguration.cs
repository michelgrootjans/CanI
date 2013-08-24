using System;

namespace CanI.Core
{
    public static class I
    {
        public static bool Can(string action, object subject)
        {
            var ability = AbilityConfiguration.CreateAbility();
            if (subject is string)
                return ability.Allows(action, subject as string);
            return ability.Allows(action, subject.GetType().Name);
        }
    }

    public static class AbilityConfiguration
    {
        private static Action<IAbilityConfiguration> configurationApplier;

        public static void ConfigureWith(Action<IAbilityConfiguration> configuration)
        {
            AbilityConfiguration.configurationApplier = configuration;
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