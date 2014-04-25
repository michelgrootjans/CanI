using System;
using System.Web.Mvc;

namespace CanI.Mvc
{
    public class AbilityMvcConfiguration
    {
        private static ActionResult _unauthorizedAction = null;

        public static void ConfigureUnauthorizedActionResult(ActionResult unauthorizedAction)
        {
            _unauthorizedAction = unauthorizedAction;
        }

        public static ActionResult UnauthorizedActionResult()
        {
            return _unauthorizedAction ?? new HttpUnauthorizedResult();
        }
    }
}