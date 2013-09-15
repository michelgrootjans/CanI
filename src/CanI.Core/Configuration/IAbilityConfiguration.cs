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
        IPermissionConfiguration OnEverything();
    }

    public interface IPermissionConfiguration
    {
        void If(Func<bool> predicate);
        void If<T>(Func<T, bool> predicate);
    }
}