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

        public bool Allows(string action, string subject)
        {
            return permissions.Any(p => p.Allows(action, subject));
        }

        public void AllowTo(string action, string subject)
        {
            permissions.Add(new Permission(action, subject));
        }

        public IFluentAbilityActionConfiguration Allow(params string[] actions)
        {
            return new FluentAbilityActionConfiguration(actions, this);
        }
    }

    public class FluentAbilityActionConfiguration : IFluentAbilityActionConfiguration
    {
        private readonly string[] actions;
        private readonly IAbilityConfiguration ability;

        public FluentAbilityActionConfiguration(string[] actions, IAbilityConfiguration ability)
        {
            this.actions = actions;
            this.ability = ability;
        }

        public void On(params string[] subjects)
        {
            foreach (var subject in subjects)
                foreach (var action in actions)
                    ability.AllowTo(action, subject);
        }
    }
}