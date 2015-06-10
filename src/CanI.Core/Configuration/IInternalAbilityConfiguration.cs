using System;

namespace CanI.Core.Configuration
{
    internal interface IInternalAbilityConfiguration
    {
        IPermissionConfiguration AllowTo(string action, string subject);
        IPermissionConfiguration AllowTo<T>(string action, Func<T, bool> predicate) where T : class;
    }
}