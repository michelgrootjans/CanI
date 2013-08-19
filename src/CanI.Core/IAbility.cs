namespace CanI.Core
{
    public interface IAbility
    {
        bool Allows(string action, string subject);
    }
}