using System.Web;
using System.Web.Mvc;

namespace CanI.Demo.Controllers
{
    public class AuthorizeWithCanIAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }
    }
}