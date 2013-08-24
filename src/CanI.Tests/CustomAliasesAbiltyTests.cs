using CanI.Core;
using NUnit.Framework;

namespace CanI.Tests
{
    [TestFixture]
    public class CustomAliasesAbiltyTests
    {
        [Test]
        public void unconfigured_alias_doesnt_work()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "customer"));
            Then.IShouldNotBeAbleTo("consult", "customer");
        }

        [Test]
        public void configured_alias_works()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("view", "customer");
                c.ConfigureActionAlias("view", "consult");
            });
            Then.IShouldBeAbleTo("consult", "customer");
        }
    }
}