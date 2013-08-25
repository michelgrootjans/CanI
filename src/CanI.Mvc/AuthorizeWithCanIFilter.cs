using System;
using System.Web.Mvc;
using CanI.Core;

namespace CanI.Mvc
{
    public class AuthorizeWithCanIFilter : IAuthorizationFilter
    {
        private readonly ActionResult resultOnAuthorizationFailure;

        public AuthorizeWithCanIFilter(ActionResult resultOnAuthorizationFailure)
        {
            this.resultOnAuthorizationFailure = resultOnAuthorizationFailure;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var ability = AbilityConfiguration.CreateAbility();
            if (ability == null)
                throw new Exception("CanIMvcConfiguration has not been configured.");

            var action = filterContext.ActionDescriptor.ActionName;
            var subject = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (ability.Allows(action, subject)) return;

            filterContext.Result = resultOnAuthorizationFailure;
        }
    }
}