using System;
using System.Web.Mvc;
using CanI.Core.Configuration;

namespace CanI.Mvc
{
    public class AuthorizeWithCanIFilter : AuthorizeAttribute
    {
        private readonly Func<AuthorizationContext, ActionResult> resultOnFailedAuthorization;

        public AuthorizeWithCanIFilter(Func<AuthorizationContext, ActionResult> resultOnFailedAuthorization)
        {
            this.resultOnFailedAuthorization = resultOnFailedAuthorization;
        }

        public AuthorizeWithCanIFilter(string redirectUrl)
            : this(context => new RedirectResult(redirectUrl))
        {
        }

        public AuthorizeWithCanIFilter()
            : this("/")
        {
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var ability = AbilityConfiguration.CreateAbility();
            if (ability == null)
                throw new Exception("AbilityConfiguration has not been configured.");

            var action = filterContext.ActionDescriptor.ActionName;
            var subject = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (ability.Allows(action, subject)) return;

            filterContext.Result = resultOnFailedAuthorization(filterContext);
        }
    }
}