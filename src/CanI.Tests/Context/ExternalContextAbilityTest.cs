using CanI.Core;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Context
{
    [TestFixture]
    public class ExternalContextAbilityTest
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void with_positive_context_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c => 
                c.AllowTo("view", "order")
                 .If(() => true)
                 );
            Then.IShouldBeAbleTo("view", "order");
        }

        [Test]
        public void with_negative_context_denies_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.AllowTo("view", "order")
                 .If(() => false)
                 );
            Then.IShouldNotBeAbleTo("view", "order");
        }

        [Test]
        public void with_subject_sensitive_context_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.AllowTo("view", "order")
                 .If(() => true)
                 );
            Then.IShouldBeAbleTo("view", "order");
        }

    }
}