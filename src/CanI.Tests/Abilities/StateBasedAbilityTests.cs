using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Abilities
{
    [TestFixture]
    public class StateBasedAbilityTests
    {
        private class Order
        {
            public bool CanSend { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void positive_state_allows_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("send").On("order"));
            Then.IShouldBeAbleTo("send", new Order{CanSend = true});
        }

        [Test]
        public void negative_state_denies_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("send").On("order"));
            Then.IShouldNotBeAbleTo("send", new Order { CanSend = false });
        }

        [Test]
        public void negative_state_denies_action__even_with_manage_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowAnything().OnEverything());
            Then.IShouldNotBeAbleTo("send", new Order { CanSend = false });
        }

    }
}