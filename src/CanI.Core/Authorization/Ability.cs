using System;
using System.Collections.Generic;
using System.Linq;
using CanI.Core.Cleaners;
using CanI.Core.Configuration;

namespace CanI.Core.Authorization
{
    public class Ability : IAbility, IAbilityConfiguration, IInternalAbilityConfiguration
    {
        private readonly Action<string> debugAction;
        private readonly ICollection<Permission> permissions;
        private readonly ActionCleaner actionCleaner;
        private readonly SubjectCleaner subjectCleaner;
        private bool logVerbose;

        public Ability(Action<string> debugAction, bool logVerbose)
        {
            this.debugAction = debugAction;
            permissions = new List<Permission>();
            actionCleaner = new ActionCleaner();
            subjectCleaner = new SubjectCleaner();
            this.logVerbose = logVerbose;
        }

        public bool Allows(string requestedAction, object requestedSubject)
        {
            var permission = permissions.Any(p => p.Authorizes(requestedAction, requestedSubject));
            if (permission)
                debugAction(string.Format("user can {0}/{1}", requestedAction, requestedSubject));
            else
                debugAction(string.Format("user cannot {0}/{1}", requestedAction, requestedSubject));
            return permission;
        }

        public bool AllowsExecutionOf(object command)
        {
            return permissions.Any(p => p.AllowsExecutionOf(command));
        }

        public IPermissionConfiguration AllowTo(string action, string subject)
        {
            var permission = new Permission(action, subject, actionCleaner, subjectCleaner);
            permissions.Add(permission);
            if(logVerbose)
                debugAction.Invoke(string.Format("user has the ability to {0}/{1}", action, subject));
            return permission;
        }

        public IActionConfiguration Allow(params string[] actions)
        {
            return new ActionConfiguration(actions, this);
        }

        public IActionConfiguration AllowAnything()
        {
            return new ActionConfiguration(new[] { ".+" }, this); // that's regex for 'anything'
        }

        public void ConfigureActionAliases(string intendedAction, params string[] aliases)
        {
            actionCleaner.ConfigureAliases(intendedAction, aliases);
        }

        public void ConfigureSubjectAliases(string intendedSubject, params string[] aliases)
        {
            subjectCleaner.AddSubjectAliases(intendedSubject, aliases);
        }
    }
}