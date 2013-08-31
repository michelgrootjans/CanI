using System;

namespace CanI.Core
{
    public interface IAbilityConfiguration
    {
        IPermissionConfiguration AllowTo(string action, string subject);
        IFluentAbilityActionConfiguration Allow(params string[] actions);
        void IgnoreSubjectPostfix(string postfix);
        void ConfigureActionAlias(string intendedAction, params string[] aliases);
        void ConfigureSubjectAliases(string intendedSubject, params string[] aliases);
    }

    public interface IPermissionConfiguration
    {
        void If(Func<bool> predicate);
    }
}