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

        public bool Allows(string action, string subject)
        {
            return permissions.Any(p => p.Allows(action, subject));
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

        public bool Allows(string action, string subject)
        {
            var actionIsAllowed = MatchesAction(action.ToLower());
            var subjectIsAllowed = MatchesSubject(subject.ToLower());
            return actionIsAllowed && subjectIsAllowed;
        }

        private bool MatchesAction(string action)
        {
            if (this.action == "manage") 
                return true;

            return this.action == TranslateAction(action);
        }

        private bool MatchesSubject(string subject)
        {
            if (this.subject == "all") 
                return true;

            return this.subject == subject;
        }

        private static string TranslateAction(string action)
        {
            switch (action)
            {
                case "index":
                case "detail":
                case "show":
                    return "view";
                default:
                    return action;
            }
        }
    }

    public interface IAbilityConfigurator
    {
        void Configure(IAbilityConfiguration userConfiguration);
    }
}