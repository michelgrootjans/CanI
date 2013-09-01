using CanI.Core.Configuration;

namespace CanI.Core.Authorization
{
    public static class I
    {
        public static bool Can(string action, object subject)
        {
            var ability = AbilityConfiguration.CreateAbility();
            return ability.Allows(action, subject);
        }
    }
}