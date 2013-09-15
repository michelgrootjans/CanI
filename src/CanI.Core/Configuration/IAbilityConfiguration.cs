using System;

namespace CanI.Core.Configuration
{
    public interface IAbilityConfiguration
    {
        [Obsolete("This method will soon be removed. Please use Allow('action').On('subject')")]
        IPermissionConfiguration AllowTo(string action, string subject);

        IAbilityActionConfiguration Allow(params string[] actions);
        IAbilityActionConfiguration AllowAnything();
        
        void ConfigureActionAliases(string intendedAction, params string[] aliases);
        void ConfigureSubjectAliases(string intendedSubject, params string[] aliases);
    }

    public interface IAbilityActionConfiguration
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