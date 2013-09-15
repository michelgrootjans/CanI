using CanI.Core.Configuration;
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
            AbilityConfiguration.ConfigureWith(c => c.Allow("create").On("customer"));
            Then.IShouldBeAbleTo("insert", "customer");
        }
    }
}