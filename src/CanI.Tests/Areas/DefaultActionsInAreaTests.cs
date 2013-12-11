using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Areas
{
    [TestFixture]
    public class DefaultActionsInAreaTests
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void default_area_behavior()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("area/view").On("customer"));
            Then.IShouldNotBeAbleTo("view", "customer");
            Then.IShouldBeAbleTo("area/view", "customer");
        }

        [Test]
        public void area_is_case_insensitive()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("aREa/vIEw").On("customer"));
            Then.IShouldBeAbleTo("AreA/View", "customer");
        }

        public class CustomerDto{}
        [Test]
        public void area_behavior_on_model()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("area/view").On("customer"));
            Then.IShouldNotBeAbleTo("view", new CustomerDto());
            Then.IShouldBeAbleTo("area/view", new CustomerDto());
        }

    }
}