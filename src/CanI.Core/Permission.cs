using System;
using System.Collections.Generic;
using System.Linq;

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
        public bool Allows(string action, string subject)
        {
            return MatchesAction(action.ToLower()) 
                && MatchesSubject(subject.ToLower())
                && ContextAllowsAction();
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

        private bool ContextAllowsAction()
        {
            return positivePredicates.All(p => p());
        }

    }
}