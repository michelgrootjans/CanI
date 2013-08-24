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
        private static Func<IAbilityConfigurator> configuratorFactory;
        private static Action<IAbilityConfiguration> configurationFactory;

        public static void ConfigureWith(Func<IAbilityConfigurator> factory)
        {
            configuratorFactory = factory;
        }

        public static void ConfigureWith(Action<IAbilityConfiguration> configuration)
        {
            configurationFactory = configuration;
        }

        public static IAbility CreateAbility()
        {
            if (configuratorFactory != null)
                return new Ability(configuratorFactory());
            var ability = new Ability();
            if(configurationFactory != null)
                configurationFactory(ability);
            return ability;
        }

        internal static void Reset()
        {
            configurationFactory = null;
            configuratorFactory = null;
        }
    }
}