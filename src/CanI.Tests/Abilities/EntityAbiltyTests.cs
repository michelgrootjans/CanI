using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Abilities
{
    [TestFixture]
    public class EntityAbiltyTests
    {
        private class Customer { }

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void an_ability_can_be_checked_with_an_entity()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("Customer"));
            Then.IShouldBeAbleTo("view", new Customer());
        }

        [Test]
        public void an_ability_can_be_checked_with_an_entity_case_insensitive()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("customer"));
            Then.IShouldBeAbleTo("view", new Customer());
        }
    }
}