using System;
using System.Web.Mvc;
using CanI.Core.Authorization;
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

            var actionAndSubject = GetActionAndSubject(filterContext);

            if (ability.Allows(actionAndSubject.Action, actionAndSubject.Subject)) 
                return;

            filterContext.Result = resultOnFailedAuthorization(filterContext);
        }

        private ActionAndSubject GetActionAndSubject(AuthorizationContext filterContext)
        {
            var customAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof (AuthorizeIfICanAttribute), true);
            if (customAttributes.Length > 0)
            {
                var attribute = customAttributes[0] as AuthorizeIfICanAttribute;
                if (attribute != null) 
                    return new ActionAndSubject(attribute.Action, attribute.Subject);
            }

            var requestAction = filterContext.ActionDescriptor.ActionName;
            var requestSubject = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                var area = filterContext.RouteData.DataTokens["area"];
                requestAction = string.Format("{0}/{1}", area, requestAction);
            }
            return new ActionAndSubject(requestAction, requestSubject);
        }

        private class ActionAndSubject
        {
            public string Action { get; private set; }
            public string Subject { get; private set; }

            public ActionAndSubject(string action, string subject)
            {
                Action = action;
                Subject = subject;
            }
        }
    }
}