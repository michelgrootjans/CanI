using System;

namespace CanI.Core.Configuration
{
    public interface IAbilityConfiguration
    {
        IPermissionConfiguration AllowTo(string action, string subject);
        IAbilityActionConfiguration Allow(params string[] actions);
        IAbilityActionConfiguration AllowAll();
        
        void ConfigureActionAliases(string intendedAction, params string[] aliases);
        void ConfigureSubjectAliases(string intendedSubject, params string[] aliases);
    }

    public interface IAbilityActionConfiguration
    {
        void On(params string[] subjects);
        void OnEverything();
    }

    public interface IPermissionConfiguration
    {
        void If(Func<bool> predicate);
        void If<T>(Func<T, bool> predicate);
    }
}