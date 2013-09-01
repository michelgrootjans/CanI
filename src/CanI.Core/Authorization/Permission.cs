using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CanI.Core.Cleaners;
using CanI.Core.Configuration;
using CanI.Core.Predicates;

namespace CanI.Core.Authorization
{
    public class Permission : IPermissionConfiguration
    {
        private readonly string action;
        private readonly string subject;
        private readonly ActionCleaner actionCleaner;
        private readonly SubjectCleaner subjectCleaner;
        private readonly IList<IAuthorizationPredicate> authorizationPredicates;

        public Permission(string action, string subject, ActionCleaner actionCleaner, SubjectCleaner subjectCleaner)
        {
            this.actionCleaner = actionCleaner;
            this.subjectCleaner = subjectCleaner;
            this.action = actionCleaner.Clean(action);
            this.subject = subjectCleaner.Clean(subject);

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
            if (action == "manage") 
                return true;

            return action == actionCleaner.Clean(requestedAction);
        }

        private bool MatchesSubject(object requestedSubject)
        {
            if (subject == "all") 
                return true;

            return subject == subjectCleaner.Clean(requestedSubject);
        }


        private bool ContextAllowsAction(object requestedSubject)
        {
            return authorizationPredicates.All(p => p.Allows(requestedSubject));
        }

        private bool SubjectAllowsAction(object requestedSubject)
        {
            const BindingFlags caseInsensitivePublicInstance = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
            var property = requestedSubject.GetType().GetProperty("can" + action, caseInsensitivePublicInstance);
            if (property == null) return true;

            var propertyValue = property.GetValue(requestedSubject);
            var booleanValue = propertyValue as bool?;
            return booleanValue.GetValueOrDefault();
        }
    }
}