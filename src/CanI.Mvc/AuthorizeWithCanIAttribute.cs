using System;
using System.Web.Mvc;
using CanI.Core;

namespace CanI.Mvc
{
    public class AuthorizeWithCanIAttribute : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var ability = CanIMvcConfiguration.CreateAbility();
            if (ability == null)
            {
                throw new Exception("CanIConfiguration has not been configured with an IMvcAbilty");
            }

            var action = filterContext.ActionDescriptor.ActionName;
            var subject = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (ability.ICan(action, subject)) return;

            filterContext.Result = ability.OnAuthorizationFailed();
        }
    }

    public static class CanIMvcConfiguration
    {
        private static Func<ActionResult> onFailedAuthorizationResultFactory;

        public static IMvcAbility CreateAbility()
        {
            return new MvcAbility(CanIConfiguration.CreateAbility(), onFailedAuthorizationResultFactory());
        }

        public static void ConfigureWith(Func<IAbilityConfigurator> configurator, Func<ActionResult> onFailedAuthorization)
        {
            CanIConfiguration.ConfigureWith(configurator);
            onFailedAuthorizationResultFactory = onFailedAuthorization;
        }
    }

    public class MvcAbility : IMvcAbility
    {
        private readonly IAbility ability;
        private readonly ActionResult onFailedAuthorizationResult;

        public MvcAbility(IAbility ability, ActionResult onFailedAuthorizationResult)
        {
            this.ability = ability;
            this.onFailedAuthorizationResult = onFailedAuthorizationResult;
        }

        public bool ICan(string action, string subject)
        {
            return ability.ICan(action, subject);
        }

        public ActionResult OnAuthorizationFailed()
        {
            return onFailedAuthorizationResult;
        }
    }
}