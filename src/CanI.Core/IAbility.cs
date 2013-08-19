namespace CanI.Core
{
    public interface IAbility
    {
        bool Can(string action, string subject);
    }
}