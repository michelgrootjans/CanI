using System;
using System.Web.Mvc;

namespace CanI.Mvc
{
    public class AuthorizeWithCanIFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var ability = CanIMvcConfiguration.CreateAbility();
            if (ability == null)
            {
                throw new Exception("CanIMvcConfiguration has not been configured.");
            }

            var action = filterContext.ActionDescriptor.ActionName;
            var subject = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (ability.Allows(action, subject)) return;

            filterContext.Result = ability.OnAuthorizationFailed();
        }
    }
}