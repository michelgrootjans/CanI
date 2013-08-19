using System;
using System.Web.Mvc;
using CanI.Demo.Controllers;

namespace CanI.Demo.Controllers
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

    public interface IMvcAbility : IAbility
    {
        ActionResult OnAuthorizationFailed();
    }

    public interface IAbility
    {
        bool Can(string action, string subject);
    }

    public static class CanIHelper{

        private static Func<IAbility> abilityFactory;

        public static bool Can(this HtmlHelper html, string action, string subject)
        {
            var ability = abilityFactory();
            return ability.Can(action, subject);
        }

        public static void ConfigureWith(Func<IAbility> factory)
        {
            abilityFactory = factory;
        }
    }
}