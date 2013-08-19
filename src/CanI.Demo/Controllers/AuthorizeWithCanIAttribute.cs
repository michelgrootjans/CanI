using System;
using System.Web.Mvc;

namespace CanI.Demo.Controllers
{
    public class AuthorizeWithCanIAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Console.WriteLine(filterContext.ActionDescriptor);
        }
    }
}