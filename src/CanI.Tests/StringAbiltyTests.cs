using CanI.Core;
using NUnit.Framework;

namespace CanI.Tests
{
    [TestFixture]
    public class StringAbiltyTests
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void a_null_abiltiy_doesnt_allow_anything()
        {
            Then.IShouldNotBeAbleTo("view", "customer");
        }

        [Test]
        public void a_simple_ability_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "customer"));
            Then.IShouldBeAbleTo("view", "customer");
        }
    }
}
