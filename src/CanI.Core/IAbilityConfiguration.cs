using System;

namespace CanI.Core
{
    public interface IAbilityConfiguration
    {
        IPermissionConfiguration AllowTo(string action, string subject);
        IFluentAbilityActionConfiguration Allow(params string[] actions);
        
        void ConfigureActionAliases(string intendedAction, params string[] aliases);

        void IgnoreSubjectPostfixes(params string[] postfixes);
        void ConfigureSubjectAliases(string intendedSubject, params string[] aliases);
    }

    public interface IPermissionConfiguration
    {
        void If(Func<bool> predicate);
        void If<T>(Func<T, bool> predicate);
    }
}