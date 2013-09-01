using System.Collections.Generic;

namespace CanI.Core
{
    public class Ability : IAbility, IAbilityConfiguration
    {
        private readonly List<Permission> permissions;
        private readonly ActionCleaner actionCleaner;
        private readonly SubjectCleaner subjectCleaner;

        public Ability()
        {
            permissions = new List<Permission>();
            actionCleaner = new ActionCleaner();
            subjectCleaner = new SubjectCleaner();
        }

        public bool Allows(string action, object subject)
        {
            var permission = permissions.Find(p => p.Authorizes(actionCleaner.Clean(action), subjectCleaner.Clean(subject)));
            if (permission == null) 
                return false;
            return permission.IsAllowedOn(subject);
        }

        public IPermissionConfiguration AllowTo(string action, string subject)
        {
            var permission = new Permission(action, subject);
            permissions.Add(permission);
            return permission;
        }

        public IFluentAbilityActionConfiguration Allow(params string[] actions)
        {
            return new FluentAbilityActionConfiguration(actions, this);
        }

        public void ConfigureActionAliases(string intendedAction, params string[] aliases)
        {
            actionCleaner.ConfigureAliases(intendedAction, aliases);
        }

        public void ConfigureSubjectAliases(string intendedSubject, params string[] aliases)
        {
            subjectCleaner.AddSubjectAliases(intendedSubject, aliases);
        }

        public void IgnoreSubjectPostfixes(params string[] postfixes)
        {
            subjectCleaner.IgnorePostfix(postfixes);
        }
    }
}