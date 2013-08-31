using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CanI.Core
{
    public class Permission : IPermissionConfiguration
    {
        private readonly string action;
        private readonly string subject;
        private readonly IList<IAuthorizationPredicate> authorizationPredicates;

        public Permission(string action, string subject)
        {
            this.action = action.ToLower();
            this.subject = subject.ToLower();
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

        public bool Authorizes(string action, string subject)
        {
            return MatchesAction(action.ToLower())
                   && MatchesSubject(subject.ToLower());
        }

        private bool MatchesAction(string action)
        {
            if (this.action == "manage") 
                return true;

            return this.action == action;
        }

        private bool MatchesSubject(string subject)
        {
            if (this.subject == "all") 
                return true;

            return this.subject == subject;
        }


        public bool IsAllowedOn(object subject)
        {
            return ContextAllowsAction(subject) 
                && SubjectAllowsAction(subject);
        }

        private bool ContextAllowsAction(object subject)
        {
            return authorizationPredicates.All(p => p.Allows(subject));
        }

        private bool SubjectAllowsAction(object subject)
        {
            const BindingFlags caseInsensitivePublicInstance = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
            var property = subject.GetType().GetProperty("can" + action, caseInsensitivePublicInstance);
            if (property == null) return true;

            var propertyValue = property.GetValue(subject);
            var booleanValue = propertyValue as bool?;
            return booleanValue.GetValueOrDefault();
        }
    }

    public class GenericPredicate<T> : IAuthorizationPredicate
    {
        private readonly Func<T, bool> predicate;

        public GenericPredicate(Func<T, bool> predicate)
        {
            this.predicate = predicate;
        }

        public bool Allows(object subject)
        {
            if(typeof(T).IsInstanceOfType(subject))
                return predicate((T) subject);
            return true;
        }
    }

    public class PlainAuthorizationPredicate : IAuthorizationPredicate
    {
        private readonly Func<bool> predicate;

        public PlainAuthorizationPredicate(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public bool Allows(dynamic subject)
        {
            return predicate();
        }
    }

    internal interface IAuthorizationPredicate
    {
        bool Allows(object subject);
    }
}