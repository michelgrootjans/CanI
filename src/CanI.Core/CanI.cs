using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace CanI.Core
{
    public static class CanIConfiguration
    {
        private static Func<IAbilityConfigurator> configurationFactory;

        public static void ConfigureWith(Func<IAbilityConfigurator> factory)
        {
            configurationFactory = factory;
        }

        public static IAbility CreateAbility()
        {
            return new Ability(configurationFactory());
        }
    }

    public class Ability : IAbility, IAbilityConfiguration
    {
        private readonly List<Permission> permissions;

        public Ability(IAbilityConfigurator configuration)
        {
            permissions = new List<Permission>();
            configuration.Configure(this);
        }

        public void AllowTo(string action, string subject)
        {
            permissions.Add(new Permission(action, subject));
        }

        public bool ICan(string action, string subject)
        {
            return permissions.Any(p => p.ICan(action, subject));
        }

    }

    public class Permission
    {
        private readonly string action;
        private readonly string subject;

        public Permission(string action, string subject)
        {
            this.action = action.ToLower();
            this.subject = subject.ToLower();
        }

        public bool ICan(string a, string s)
        {
            var actionIsAllowed = MatchesAction(a.ToLower());
            var subjectIsAllowed = MatchesSubject(s.ToLower());
            return actionIsAllowed && subjectIsAllowed;
        }

        private bool MatchesAction(string a)
        {
            if (action == "manage") 
                return true;

            return TranslateAction(a) == action;
        }

        private bool MatchesSubject(string s)
        {
            if (subject == "all") 
                return true;

            return s == subject;
        }

        private string TranslateAction(string a)
        {
            switch (a)
            {
                case "index":
                case "detail":
                case "show":
                    return "view";
                default:
                    return a;
            }
        }
    }

    public interface IAbilityConfigurator
    {
        void Configure(IAbilityConfiguration userConfiguration);
    }
}