using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CanI.Core.Cleaners;
using CanI.Core.Configuration;
using CanI.Core.Predicates;

namespace CanI.Core.Authorization
{
    public class Permission : IPermissionConfiguration
    {
        private string Action { get; set; }
        private string Subject { get; set; }
        private readonly ActionCleaner actionCleaner;
        private readonly SubjectCleaner subjectCleaner;
        private readonly IEnumerable<string> commandConventions;
        private readonly IList<IAuthorizationPredicate> authorizationPredicates;

        public Permission(string action, string subject, ActionCleaner actionCleaner, SubjectCleaner subjectCleaner, IEnumerable<string> commandConventions)
        {
            this.actionCleaner = actionCleaner;
            this.subjectCleaner = subjectCleaner;
            this.commandConventions = commandConventions;
            Action = actionCleaner.Clean(action);
            Subject = subjectCleaner.Clean(subject);

            authorizationPredicates = new List<IAuthorizationPredicate>();
        }

        public void If(Func<bool> predicate)
        {
            authorizationPredicates.Add(new PlainAuthorizationPredicate(predicate));
        }

        public void If<T>(Func<T, bool> predicate)
        {
            authorizationPredicates.Add(new GenericPredicate<T>(predicate));
        }

        public bool Authorizes(string requestedAction, object requestedSubject)
        {
            return MatchesAction(requestedAction)
                   && MatchesSubject(requestedSubject)
                   && ContextAllowsAction(requestedSubject) 
                   && SubjectAllowsAction(requestedSubject);
        }

        private bool MatchesAction(string requestedAction)
        {
            if (Action == "manage") 
                return true;

            return Action == actionCleaner.Clean(requestedAction);
        }

        private bool MatchesSubject(object requestedSubject)
        {
            if (Subject == "all") 
                return true;

            return Subject == subjectCleaner.Clean(requestedSubject);
        }


        private bool ContextAllowsAction(object requestedSubject)
        {
            return authorizationPredicates.All(p => p.Allows(requestedSubject));
        }

        private bool SubjectAllowsAction(object requestedSubject)
        {
            const BindingFlags caseInsensitivePublicInstance = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
            var property = requestedSubject.GetType().GetProperty("can" + Action, caseInsensitivePublicInstance);
            if (property == null) return true;

            var propertyValue = property.GetValue(requestedSubject);
            var booleanValue = propertyValue as bool?;
            return booleanValue.GetValueOrDefault();
        }

        public bool AllowsExecutionOf(object command)
        {
            var requestedCommand = command.GetType().Name;

            //I prefer foreach over LINQ in this case
            foreach (var actionAlias in actionCleaner.AliasesFor(Action))
            {
                foreach (var commandConvention in commandConventions)
                {
                    var allowedCommand =
                        commandConvention
                            .Replace("{action}", actionAlias == "manage" ? ".+" : actionAlias)
                            .Replace("{subject}", Subject == "all" ? ".+" : Subject);

                    if (Regex.IsMatch(requestedCommand, allowedCommand, RegexOptions.IgnoreCase))
                        return true;
                }
            }

            return false;
        }
    }
}