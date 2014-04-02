using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Caching
{
    [TestFixture]
    public class CacheConfigurationTests
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void configuration_runs_only_once_with_a_static_cache()
        {
            var configurationCounter = 0;
            AbilityConfiguration.ConfigureCache(new StaticCache());
            AbilityConfiguration.ConfigureWith(c => {
                c.Allow("view").On("customer");
                configurationCounter++;
            });
            Then.IShouldBeAbleTo("view", "customer");
            Then.IShouldBeAbleTo("view", "customer");

            Assert.That(configurationCounter, Is.EqualTo(1));
        }
    }

}
