using System.Web.Mvc;

namespace CanI.Demo.Controllers
{
    public class AuthorizeWithCanIAttribute : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;

            if (controller == "Home" && action == "Index") return;

            filterContext.Result = new RedirectResult("/");
        }
    }
}