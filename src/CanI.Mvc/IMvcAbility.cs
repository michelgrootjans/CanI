using System.Web.Mvc;
using CanI.Core;

namespace CanI.Mvc
{
    public interface IMvcAbility : IAbility
    {
        ActionResult OnAuthorizationFailed();
    }
}