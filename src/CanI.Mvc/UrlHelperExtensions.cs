using System.Web.Mvc;
using CanI.Core.Authorization;

namespace CanI.Mvc
{
    public static class UrlHelperExtensions
    {
        public static bool Can(this UrlHelper helper, string action, object subject)
        {
            if (HasArea(helper))
            {
                var area = GetAreaFrom(helper);
                action = string.Format("{0}/{1}", area, action);
            }
            return I.Can(action, subject);
        }

        private static bool HasArea(UrlHelper helper)
        {
            return helper.RequestContext.RouteData.DataTokens.ContainsKey("area");
        }

        private static string GetAreaFrom(UrlHelper helper)
        {
            return helper.RequestContext.RouteData.DataTokens["area"] as string;
        }

    }
}