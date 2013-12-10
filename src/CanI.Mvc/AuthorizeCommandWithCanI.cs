using System;
using System.Security;
using System.Web.Mvc;
using CanI.Core.Configuration;

namespace CanI.Mvc
{
    public class AuthorizeCommandWithCanI : AuthorizeAttribute
    {
        private readonly string exceptionMessage;

        public AuthorizeCommandWithCanI() : this("You are not allowed to execute this action.") {}

        public AuthorizeCommandWithCanI(string exceptionMessage)
        {
            this.exceptionMessage = exceptionMessage;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var ability = AbilityConfiguration.CreateAbility();
            if (ability == null)
                throw new Exception("AbilityConfiguration has not been configured.");

            var command = GetCommandFrom(filterContext);

            if (ability.AllowsExecutionOf(command))
                return;

            throw new SecurityException(exceptionMessage);
        }

        private object GetCommandFrom(AuthorizationContext filterContext)
        {
            try
            {
                return filterContext.ActionDescriptor.GetParameters()[0].ParameterType.Name;
            }
            catch
            {
                throw new Exception("No paramater found in controller action");
            }
        }
    }
}