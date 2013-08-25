using CanI.Core;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Abilities
{
    [TestFixture]
    public class StateBasedAbilityTests
    {
        public class Order
        {
            public bool CanSend { get; set; }
        }

        [Test]
        public void positive_state_allows_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("send", "order"));
            Then.IShouldBeAbleTo("send", new Order{CanSend = true});
        }

        [Test]
        public void negative_state_denies_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("send", "order"));
            Then.IShouldNotBeAbleTo("send", new Order{CanSend = false});
        }

    }
}