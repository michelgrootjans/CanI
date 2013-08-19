using System.Web.Mvc;

namespace CanI.Demo.Controllers
{
    // Warning: this class will be a singleton for the whole application !!
    public class AuthorizeWithCanIAttribute : IAuthorizationFilter
    {
        private readonly IMvcAbility configuration;

        public AuthorizeWithCanIAttribute(IMvcAbility configuration)
        {
            this.configuration = configuration;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;

            if (configuration.Can(action, controller)) return;

            filterContext.Result = configuration.OnAuthorizationFailed();
        }
    }

    public interface IMvcAbility : IAbility
    {
        ActionResult OnAuthorizationFailed();
    }

    public interface IAbility
    {
        bool Can(string action, string controller);
    }
}