using System;

namespace CanI.Core.Predicates
{
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
}