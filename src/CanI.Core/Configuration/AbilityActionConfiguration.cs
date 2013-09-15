using CanI.Core.Authorization;

namespace CanI.Core.Configuration
{
    internal class ActionConfiguration : IActionConfiguration
    {
        private readonly string[] actions;
        private readonly Ability abilityConfiguration;

        public ActionConfiguration(string[] actions, Ability abilityConfiguration)
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

        public IPermissionConfiguration OnEverything()
        {
            return On(".+");
        }
    }
}