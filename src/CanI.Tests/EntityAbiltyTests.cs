using CanI.Core;
using NUnit.Framework;

namespace CanI.Tests
{
    [TestFixture]
    public class EntityAbiltyTests
    {
        private class Customer
        {
        }

        [Test]
        public void an_ability_can_be_checked_with_an_entity()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "Customer"));
            Then.IShouldBeAbleTo("view", new Customer());
        }

        [Test]
        public void an_ability_can_be_checked_with_an_entity_case_insensitive()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "customer"));
            Then.IShouldBeAbleTo("view", new Customer());
        }

    }
}