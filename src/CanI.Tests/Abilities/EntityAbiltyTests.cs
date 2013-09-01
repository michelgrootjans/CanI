using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Abilities
{
    [TestFixture]
    public class EntityAbiltyTests
    {
        private class Customer
        {
        }

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
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

        [Test]
        public void an_ability_can_be_checked_with_a_subject_alias()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("view", "customer");
                c.ConfigureSubjectAliases("customer", "customers");
            });
            Then.IShouldBeAbleTo("view", "customers");
        }

    }
}