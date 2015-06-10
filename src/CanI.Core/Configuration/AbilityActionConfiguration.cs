using System;

namespace CanI.Core.Configuration
{
    internal class ActionConfiguration : IActionConfiguration
    {
        private readonly string[] actions;
        private readonly IInternalAbilityConfiguration abilityConfiguration;

        public ActionConfiguration(string[] actions, IInternalAbilityConfiguration abilityConfiguration)
        {
            this.actions = actions;
            this.abilityConfiguration = abilityConfiguration;
        }

        public IPermissionConfiguration On(params string[] subjects)
        {
            var configurations = new MultiplePermissionConfiguration();
            foreach (var subject in subjects)
                foreach (var action in actions)
                    configurations.Add(abilityConfiguration.AllowTo(action, subject));

            return configurations;
        }

        public void On<T>(Func<T, bool> predicate)
        {
            On((typeof(T).Name)).If(predicate); //TODO: must match only on type, not on string
        }

        public IPermissionConfiguration OnEverything()
        {
            return On(".+"); // that's regex for 'anything'
        }
    }
}