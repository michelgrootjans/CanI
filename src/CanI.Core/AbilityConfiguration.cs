using System;

namespace CanI.Core
{
    public static class I
    {
        public static bool Can(string action, string subject)
        {
            var ability = AbilityConfiguration.CreateAbility();
            return ability.Allows(action, subject);
        }
    }

    public static class AbilityConfiguration
    {
        private static Func<IAbilityConfigurator> configurationFactory;

        public static void ConfigureWith(Func<IAbilityConfigurator> factory)
        {
            configurationFactory = factory;
        }

        public static IAbility CreateAbility()
        {
            return new Ability(configurationFactory());
        }
    }
}