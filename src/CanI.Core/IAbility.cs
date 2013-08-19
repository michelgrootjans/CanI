namespace CanI.Core
{
    public interface IAbility
    {
        bool ICan(string action, string subject);
    }

    public interface IAbilityConfiguration
    {
        void AllowTo(string action, string subject);
    }
}