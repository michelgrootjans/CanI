using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Aliasing
{
    [TestFixture]
    public class DefaultEditAliasesAbiltyTests
    {
        [Test]
        public void update_is_an_alias_for_edit()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("edit").On("customer"));
            Then.IShouldBeAbleTo("update", "customer");
        }

        [Test]
        public void change_is_an_alias_for_edit()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("edit").On("customer"));
            Then.IShouldBeAbleTo("change", "customer");
        }
    }
}