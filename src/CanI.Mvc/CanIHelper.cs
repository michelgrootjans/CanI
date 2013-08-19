using System.Web.Mvc;
using CanI.Core;

namespace CanI.Mvc
{
    public static class CanIHelper{

        public static bool Can(this HtmlHelper html, string action, string subject)
        {
            var ability = CanIConfiguration.CreateAbility();
            return ability.Can(action, subject);
        }
    }
}