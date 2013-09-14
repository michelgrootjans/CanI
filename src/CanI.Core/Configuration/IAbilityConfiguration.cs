using System;

namespace CanI.Core.Configuration
{
    public interface IAbilityConfiguration
    {
        IPermissionConfiguration AllowTo(string action, string subject);
        IFluentAbilityActionConfiguration Allow(params string[] actions);
        IFluentAbilityActionConfiguration AllowAll();
        
        void IgnoreSubjectPostfixes(params string[] postfixes);
        void ConfigureActionAliases(string intendedAction, params string[] aliases);
        void ConfigureSubjectAliases(string intendedSubject, params string[] aliases);
    }

    public interface IPermissionConfiguration
    {
        void If(Func<bool> predicate);
        void If<T>(Func<T, bool> predicate);
    }

    public interface IFluentAbilityActionConfiguration
    {
        void On(params string[] subjects);
        void OnEverything();
    }
}