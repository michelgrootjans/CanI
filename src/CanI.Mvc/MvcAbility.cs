using System.Web.Mvc;
using CanI.Core;

namespace CanI.Mvc
{
    public class MvcAbility : IMvcAbility
    {
        private readonly IAbility ability;
        private readonly ActionResult onFailedAuthorizationResult;

        public MvcAbility(IAbility ability, ActionResult onFailedAuthorizationResult)
        {
            this.ability = ability;
            this.onFailedAuthorizationResult = onFailedAuthorizationResult;
        }

        public bool Allows(string action, string subject)
        {
            return ability.Allows(action, subject);
        }

        public ActionResult OnAuthorizationFailed()
        {
            return onFailedAuthorizationResult;
        }
    }
}