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

        public Ability()
        {
            permissions = new List<Permission>();
            ignoredPostfixes = new List<string>();
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

        public Ability(IAbilityConfigurator configurator) : this()
        {
            if(configurator != null) 
                configurator.Configure(this);
        }

        public bool Allows(string action, string subject)
        {
            var cleanedSubject = CleanupSubject(subject);
            var cleanAction = CleanupAction(action);

            return permissions.Any(p => p.Allows(cleanAction, cleanedSubject));
        }

        private string CleanupAction(string action)
        {
            var lowerCaseAction = action.ToLower();
            if(actionAliases.ContainsKey(lowerCaseAction))
                return actionAliases[lowerCaseAction];
            return lowerCaseAction;
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

        private string CleanupSubject(string subject)
        {
            var lowerCaseSubject = subject.ToLower();
            var matchingPostfix = ignoredPostfixes.FirstOrDefault(lowerCaseSubject.EndsWith);
            if(matchingPostfix != null)
                return lowerCaseSubject.Replace(matchingPostfix, "");
            return lowerCaseSubject;
        }
    }
}