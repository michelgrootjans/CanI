namespace CanI.Core
{
    public interface IFluentAbilityActionConfiguration
    {
        void On(params string[] subjects);
    }

    internal class FluentAbilityActionConfiguration : IFluentAbilityActionConfiguration
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