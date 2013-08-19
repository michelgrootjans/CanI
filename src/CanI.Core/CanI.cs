using System;

namespace CanI.Core
{
    public static class CanIConfiguration
    {
        private static Func<IAbility> abilityFactory;

        public static void ConfigureWith(Func<IAbility> factory)
        {
            abilityFactory = factory;
        }

        public static IAbility CreateAbility()
        {
            return abilityFactory();
        }
    }
}