using CanI.Core;
using NUnit.Framework;

namespace CanI.Tests
{
    [TestFixture]
    public class DefaultEditAliasesAbiltyTests
    {
        [Test]
        public void edit_just_works()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("edit", "customer"));
            Then.IShouldBeAbleTo("edit", "customer");
        }

        [Test]
        public void update_is_an_alias_for_edit()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("edit", "customer"));
            Then.IShouldBeAbleTo("update", "customer");
        }
    }
}