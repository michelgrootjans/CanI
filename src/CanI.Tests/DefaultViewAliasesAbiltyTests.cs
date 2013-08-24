using CanI.Core;
using NUnit.Framework;

namespace CanI.Tests
{
    [TestFixture]
    public class DefaultViewAliasesAbiltyTests
    {
        [Test]
        public void index_is_an_alias_for_view()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "customer"));
            Then.IShouldBeAbleTo("index", "customer");
        }

        [Test]
        public void show_is_an_alias_for_view()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "customer"));
            Then.IShouldBeAbleTo("show", "customer");
        }

        [Test]
        public void detail_is_an_alias_for_view()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "customer"));
            Then.IShouldBeAbleTo("detail", "customer");
        }
    }
}