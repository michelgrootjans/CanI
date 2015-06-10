using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CanI.Core.Cleaners;
using CanI.Core.Configuration;

namespace CanI.Core.Authorization
{
    public class TypedPermission<T> : Permission where T : class
    {
        public string AllowedAction { get; private set; }
        private readonly Func<T, bool> predicate;
        private readonly ActionCleaner actionCleaner;

        public TypedPermission(string action, Func<T, bool> predicate, ActionCleaner actionCleaner)
        {
            AllowedAction = actionCleaner.Clean(action);
            this.predicate = predicate;
            this.actionCleaner = actionCleaner;
        }

        public override IPermissionConfiguration If<T1>(Func<T1, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override bool Authorizes(string requestedAction, object requestedSubject)
        {
            if(requestedSubject.GetType() != typeof(T)) return false;
            return Matches(requestedAction) 
                && MatchesPredicate(requestedSubject)
                && SubjectAllowsAction(requestedAction, requestedSubject);
        }

        private bool Matches(string action)
        {
            var requestedAction = actionCleaner.Clean(action);

            return Regex.IsMatch(requestedAction, AllowedAction, RegexOptions.IgnoreCase);
        }

        private bool MatchesPredicate(object requestedSubject)
        {
            return predicate(requestedSubject as T);
        }

        private bool SubjectAllowsAction(string requestedAction, object requestedSubject)
        {
            const BindingFlags caseInsensitivePublicInstance = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
            var property = requestedSubject.GetType().GetProperty("can" + PureAction(AllowedAction), caseInsensitivePublicInstance);
            if (property == null) return true;

            var propertyValue = property.GetValue(requestedSubject, BindingFlags.Instance, null, null, null);

            var booleanValue = propertyValue as bool?;
            return booleanValue.GetValueOrDefault();
        }

        private string PureAction(string requestedAction)
        {
            return requestedAction.Split('/').Last();
        }

        public override bool AllowsExecutionOf(object command)
        {
            throw new NotImplementedException();
        }
    }
}