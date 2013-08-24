using CanI.Core;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Aliasing
{
    [TestFixture]
    public class DefaultCreateAliasesAbiltyTests
    {
        [Test]
        public void insert_is_an_alias_for_create()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("create", "customer"));
            Then.IShouldBeAbleTo("insert", "customer");
        }
    }
}