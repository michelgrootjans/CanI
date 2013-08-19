using System;
using System.Web.Mvc;
using CanI.Core;

namespace CanI.Mvc
{
    public static class CanIMvcConfiguration
    {
        private static Func<ActionResult> onFailedAuthorizationResultFactory;

        public static IMvcAbility CreateAbility()
        {
            return new MvcAbility(AbilityConfiguration.CreateAbility(), onFailedAuthorizationResultFactory());
        }

        public static void ConfigureWith(Func<IAbilityConfigurator> configurator, Func<ActionResult> onFailedAuthorization)
        {
            AbilityConfiguration.ConfigureWith(configurator);
            onFailedAuthorizationResultFactory = onFailedAuthorization;
        }
    }
}