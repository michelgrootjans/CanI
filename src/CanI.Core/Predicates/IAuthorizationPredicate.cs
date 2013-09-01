namespace CanI.Core.Predicates
{
    internal interface IAuthorizationPredicate
    {
        bool Allows(object subject);
    }
}