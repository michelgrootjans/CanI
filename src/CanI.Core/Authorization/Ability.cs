using System.Collections.Generic;
using System.Linq;
using CanI.Core.Cleaners;
using CanI.Core.Configuration;

namespace CanI.Core.Authorization
{
    public class Ability : IAbility, IAbilityConfiguration
    {
        private readonly ICollection<Permission> permissions;
        private readonly ActionCleaner actionCleaner;
        private readonly SubjectCleaner subjectCleaner;
        private string commandConvention;

        public Ability()
        {
            permissions = new List<Permission>();
            actionCleaner = new ActionCleaner();
            subjectCleaner = new SubjectCleaner();
            commandConvention = "";
        }

        public bool Allows(string action, object subject)
        {
            return permissions.Any(p => p.Authorizes(action, subject));
        }

        public bool AllowsExecutionOf(object command)
        {
            var commandName = command.GetType().Name.ToLower();
            foreach (var permission in permissions)
            {
                var permissionCommand =
                    commandConvention
                        .Replace("{action}", permission.Action)
                        .Replace("{subject}", permission.Subject);

                if (commandName == permissionCommand)
                    return true;
            }
            return false;
        }

        public IPermissionConfiguration AllowTo(string action, string subject)
        {
            var permission = new Permission(action, subject, actionCleaner, subjectCleaner);
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

        public void ConfigureCommandConvention(string commandConvention)
        {
            this.commandConvention = commandConvention.ToLower();
        }

        public void IgnoreSubjectPostfixes(params string[] postfixes)
        {
            subjectCleaner.IgnorePostfix(postfixes);
        }
    }
}