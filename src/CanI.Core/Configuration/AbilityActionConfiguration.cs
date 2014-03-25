using System;

namespace CanI.Core.Configuration
{
    internal class ActionConfiguration : IActionConfiguration
    {
        private readonly string[] actions;
        private readonly IInternalAbilityConfiguration abilityConfiguration;
        private readonly Action<string> debugAction;

        public ActionConfiguration(string[] actions, IInternalAbilityConfiguration abilityConfiguration, Action<string> debugAction)
        {
            this.actions = actions;
            this.abilityConfiguration = abilityConfiguration;
            this.debugAction = debugAction;
        }

        public IPermissionConfiguration On(params string[] subjects)
        {
            var configurations = new MultiplePermissionConfiguration();
            foreach (var subject in subjects)
                foreach (var action in actions)
                    configurations.Add(abilityConfiguration.AllowTo(action, subject));

            debugAction(string.Format("user can {0}/{1}", actions[0], subjects[0]));

            return configurations;
        }

        public IPermissionConfiguration OnEverything()
        {
            return On(".+"); // that's regex for 'anything'
        }
    }
}