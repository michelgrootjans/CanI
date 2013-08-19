using System;
using System.Web.Mvc;

namespace CanI.Mvc
{
    public class AuthorizeWithCanIAttribute : IAuthorizationFilter
    {
        // This class could be a singleton for the whole application
        // Call this factory for each authorization call
        private readonly Func<IMvcAbility> abiltiyFactory;

        public AuthorizeWithCanIAttribute(Func<IMvcAbility> abiltiyFactory)
        {
            this.abiltiyFactory = abiltiyFactory;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var ability = abiltiyFactory();
            var action = filterContext.ActionDescriptor.ActionName;
            var subject = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (ability.Can(action, subject)) return;

            filterContext.Result = ability.OnAuthorizationFailed();
        }
    }
}