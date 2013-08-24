using System;
using System.Collections.Generic;
using System.Linq;

namespace CanI.Core
{
    public class Ability : IAbility, IAbilityConfiguration
    {
        private readonly List<Permission> permissions;
        private readonly IList<string> ignoredPostfixes;

        public Ability()
        {
            permissions = new List<Permission>();
            ignoredPostfixes = new List<string>();
        }

        public Ability(IAbilityConfigurator configurator) : this()
        {
            if(configurator != null) 
                configurator.Configure(this);
        }

        public bool Allows(string action, string subject)
        {
            var cleanedSubject = RemoveAffixFrom(subject);
            return permissions.Any(p => p.Allows(action, cleanedSubject));
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

        private string RemoveAffixFrom(string subject)
        {
            var lowerCaseSubject = subject.ToLower();
            var matchingPostfix = ignoredPostfixes.FirstOrDefault(lowerCaseSubject.EndsWith);
            if(matchingPostfix != null)
                return lowerCaseSubject.Replace(matchingPostfix, "");
            return lowerCaseSubject;
        }
    }
}