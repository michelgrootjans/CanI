using System;

namespace CanI.Core.Configuration
{
    public interface IAbilityConfiguration
    {
        IActionConfiguration Allow(params string[] actions);
        IActionConfiguration AllowAnything();
        
        void ConfigureActionAliases(string intendedAction, params string[] aliases);
        void ConfigureSubjectAliases(string intendedSubject, params string[] aliases);
    }

    public interface IActionConfiguration
    {
        IPermissionConfiguration On(params string[] subjects);
        void On<T>(Func<T, bool> predicate);
        IPermissionConfiguration OnEverything();
    }

    public interface IPermissionConfiguration
    {
        IPermissionConfiguration If<T>(Func<T, bool> predicate);
    }
}