namespace CanI.Core
{
    public interface IAbilityConfiguration
    {
        void AllowTo(string action, string subject);
        IFluentAbilityActionConfiguration Allow(params string[] actions);
        void IgnoreSubjectPostfix(string postfix);
        void ConfigureActionAlias(string intendedAction, params string[] aliases);
        void ConfigureSubjectAliases(string intendedSubject, params string[] aliases);
    }
}