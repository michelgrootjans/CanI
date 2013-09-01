namespace CanI.Core
{
    internal interface IAuthorizationPredicate
    {
        bool Allows(object subject);
    }
}