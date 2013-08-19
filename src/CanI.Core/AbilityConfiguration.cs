using System;

namespace CanI.Core
{
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