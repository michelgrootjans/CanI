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
        private IList<Func<bool>> positivePredicates;

        public Permission(string action, string subject)
        {
            this.action = action.ToLower();
            this.subject = subject.ToLower();
            positivePredicates = new List<Func<bool>>();
        }

        public void If(Func<bool> predicate)
        {
            positivePredicates.Add(predicate);
        }

        //private?
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

//            return true;
            return ContextAllowsAction() 
                && SubjectAllowsAction(subject);
        }

        private bool ContextAllowsAction()
        {
            return positivePredicates.All(p => p());
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
}