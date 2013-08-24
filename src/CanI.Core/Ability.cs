using System;
using System.Collections.Generic;
using System.Linq;

namespace CanI.Core
{
    public class Ability : IAbility, IAbilityConfiguration
    {
        private readonly List<Permission> permissions;
        private readonly IList<string> ignoredPostfixes;
        private readonly IDictionary<string, string> actionAliases;
        private readonly IDictionary<string, string> subjectAliases;

        public Ability()
        {
            permissions = new List<Permission>();
            ignoredPostfixes = new List<string>();
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

        public bool Allows(string action, string subject)
        {
            var cleanedSubject = CleanupSubject(subject);
            var cleanAction = CleanupAction(action);

            return permissions.Any(p => p.Allows(cleanAction, cleanedSubject));
        }

        public void AllowTo(string action, string subject)
        {
            permissions.Add(new Permission(action, subject));
        }

        public IFluentAbilityActionConfiguration Allow(params string[] actions)
        {
            return new FluentAbilityActionConfiguration(actions, this);
        }

        public void IgnoreSubjectPostfix(string postfix)
        {
            ignoredPostfixes.Add(postfix.ToLower());
        }

        public void ConfigureActionAlias(string intendedAction, params string[] aliases)
        {
            foreach (var alias in aliases)
                actionAliases.Add(alias, intendedAction);

        }

        public void ConfigureSubjectAliases(string intendedSubject, params string[] aliases)
        {
            foreach (var alias in aliases)
                subjectAliases.Add(alias, intendedSubject);
        }

        private string CleanupSubject(string subject)
        {
            var lowerCaseSubject = subject.ToLower();
            var matchingPostfix = ignoredPostfixes.FirstOrDefault(lowerCaseSubject.EndsWith);
            if(matchingPostfix != null)
                lowerCaseSubject = lowerCaseSubject.Replace(matchingPostfix, "");
            if(subjectAliases.ContainsKey(lowerCaseSubject))
                lowerCaseSubject = subjectAliases[lowerCaseSubject];
            return lowerCaseSubject;
        }

        private string CleanupAction(string action)
        {
            var lowerCaseAction = action.ToLower();
            if (actionAliases.ContainsKey(lowerCaseAction))
                return actionAliases[lowerCaseAction];
            return lowerCaseAction;
        }
    }
}