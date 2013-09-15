namespace CanI.Core.Configuration
{
    internal class AbilityActionConfiguration : IAbilityActionConfiguration
    {
        private readonly string[] actions;
        private readonly IAbilityConfiguration ability;

        public AbilityActionConfiguration(string[] actions, IAbilityConfiguration ability)
        {
            this.actions = actions;
            this.ability = ability;
        }

        public IPermissionConfiguration On(params string[] subjects)
        {
            var configurations = new MultiplePermissionConfiguration();
            foreach (var subject in subjects)
                foreach (var action in actions)
                    configurations.Add(ability.AllowTo(action, subject));

            return configurations;
        }

        public IPermissionConfiguration OnEverything()
        {
            return On(".+");
        }
    }
}