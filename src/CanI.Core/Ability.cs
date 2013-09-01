using System.Collections.Generic;
using System.Linq;

namespace CanI.Core
{
    public class Ability : IAbility, IAbilityConfiguration
    {
        private readonly List<Permission> permissions;
        private readonly IList<string> ignoredSubjectPostfixes;
        private readonly IDictionary<string, string> actionAliases;
        private readonly IDictionary<string, string> subjectAliases;

        public Ability()
        {
            permissions = new List<Permission>();
            ignoredSubjectPostfixes = new List<string>();
            subjectAliases = new Dictionary<string, string>();
            actionAliases = new Dictionary<string, string>
            {
                {"index", "view"},
                {"show", "view"},
                {"detail", "view"},
                {"read", "view"},
                {"insert", "create"},
                {"update", "edit"},
                {"change", "edit"},
                {"remove", "delete"},
                {"destroy", "delete"}
            };
        }

        public bool Allows(string action, object subject)
        {
            var cleanedSubject = CleanupSubject(subject);
            var cleanAction = CleanupAction(action);

            var permission = permissions.Find(p => p.Authorizes(cleanAction, cleanedSubject));
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

        public void IgnoreSubjectPostfix(string postfix)
        {
            ignoredSubjectPostfixes.Add(postfix.ToLower());
        }

        public void ConfigureActionAlias(string intendedAction, params string[] aliases)
        {
            foreach (var alias in aliases)
                actionAliases.Add(alias, intendedAction);
        }

        public void ConfigureSubjectAliases(string intendedSubject, params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                subjectAliases.Add(alias.ToLower(), intendedSubject.ToLower());
            }
        }

        private string CleanupAction(string action)
        {
            var lowerCaseAction = action.ToLower();
            if (actionAliases.ContainsKey(lowerCaseAction))
                return actionAliases[lowerCaseAction];
            return lowerCaseAction;
        }

        private string CleanupSubject(object subject)
        {
            var stringSubject = SubjectToString(subject);
            stringSubject = RemoveSubjectAffixes(stringSubject);
            stringSubject = ReplaceSubjectAliases(stringSubject);
            return stringSubject;
        }

        private static string SubjectToString(object subject)
        {
            if (subject is string)
                return ((string) subject).ToLower();
            return subject.GetType().Name.ToLower();
        }

        private string RemoveSubjectAffixes(string subject)
        {
            var matchingPostfix = ignoredSubjectPostfixes.FirstOrDefault(subject.EndsWith);
            if (matchingPostfix != null)
                return  subject.Replace(matchingPostfix, "");
            return subject;
        }

        private string ReplaceSubjectAliases(string subject)
        {
            if (subjectAliases.ContainsKey(subject))
                subject = subjectAliases[subject];
            return subject;
        }
    }
}