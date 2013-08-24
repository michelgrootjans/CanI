using System;
using System.Web.Mvc;
using CanI.Core;

namespace CanI.Mvc
{
    public static class CanIMvcConfiguration
    {
        private static Func<ActionResult> onFailedAuthorizationResultFactory;

        public static void ConfigureWith(Action<IAbilityConfiguration> configuration, Func<ActionResult> onFailedAuthorization)
        {
            AbilityConfiguration.ConfigureWith(configuration);
            onFailedAuthorizationResultFactory = onFailedAuthorization;
        }

        internal static IMvcAbility CreateAbility()
        {
            return new MvcAbility(AbilityConfiguration.CreateAbility(), onFailedAuthorizationResultFactory());
        }
    }
}