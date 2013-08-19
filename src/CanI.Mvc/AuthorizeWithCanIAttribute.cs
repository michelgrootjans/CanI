using System;
using System.Web.Mvc;
using CanI.Core;

namespace CanI.Mvc
{
    public class AuthorizeWithCanIAttribute : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var ability = CanIConfiguration.CreateAbility() as IMvcAbility;
            if (ability == null)
            {
                throw new Exception("CanIConfiguration has not been configured with an IMvcAbilty");
            }

            var action = filterContext.ActionDescriptor.ActionName;
            var subject = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (ability.Can(action, subject)) return;

            filterContext.Result = ability.OnAuthorizationFailed();
        }
    }
}