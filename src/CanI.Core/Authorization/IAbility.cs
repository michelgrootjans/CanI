namespace CanI.Core.Authorization
{
    public interface IAbility
    {
        bool Allows(string action, object subject);
        bool AllowsExecutionOf(object command);
    }
}