using System.Collections.Generic;
using System.Linq;
using CanI.Core.Cleaners;
using CanI.Core.Configuration;

namespace CanI.Core.Authorization
{
    public class Ability : IAbility, IAbilityConfiguration, IInternalAbilityConfiguration
    {
        private readonly IConfigurationLogger logger;
        private readonly ICollection<Permission> permissions;
        private readonly ActionCleaner actionCleaner;
        private readonly SubjectCleaner subjectCleaner;

        public Ability(IConfigurationLogger logger)
        {
            this.logger = logger;
            permissions = new List<Permission>();
            actionCleaner = new ActionCleaner(logger);
            subjectCleaner = new SubjectCleaner(logger);
        }

        public bool Allows(string requestedAction, object requestedSubject)
        {
            var permission = permissions.Any(p => p.Authorizes(requestedAction, requestedSubject));
            if (permission)
                logger.LogExecution(string.Format("user can {0}/{1}", requestedAction, requestedSubject));
            else
                logger.LogExecution(string.Format("user cannot {0}/{1}", requestedAction, requestedSubject));
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
            logger.LogConfiguration(string.Format("user has the ability to {0}/{1}", action, subject));
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