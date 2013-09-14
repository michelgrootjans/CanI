using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Attributes
{
    [TestFixture]
    public class ClassAttributeAbiltyTests
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void a_null_abiltiy_doesnt_allow_anything()
        {
            Then.IShouldNotBeAbleToExecute(new PromoteCustomer());
        }

        [Test]
        public void a_simple_ability_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("promote", "customer"));
            Then.IShouldBeAbleToExecute(new PromoteCustomer());
        }

        private class PromoteCustomer
        {
        }
    }

}
