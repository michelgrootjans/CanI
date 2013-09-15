namespace CanI.Core.Authorization
{
    public interface IAbility
    {
        bool Allows(string requestedAction, object requestedSubject);
        bool AllowsExecutionOf(object command);
    }
}