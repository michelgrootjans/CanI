using System.Collections.Generic;
using System.Linq;

namespace CanI.Core
{
    public class Ability : IAbility, IAbilityConfiguration
    {
        private readonly List<Permission> permissions;

        public Ability(IAbilityConfigurator configuration)
        {
            permissions = new List<Permission>();
            configuration.Configure(this);
        }

        public void AllowTo(string action, string subject)
        {
            permissions.Add(new Permission(action, subject));
        }

        public bool Allows(string action, string subject)
        {
            return permissions.Any(p => p.Allows(action, subject));
        }

    }
}