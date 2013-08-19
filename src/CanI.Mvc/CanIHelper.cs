using System;
using System.Web.Mvc;
using CanI.Core;

namespace CanI.Demo.Controllers
{
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