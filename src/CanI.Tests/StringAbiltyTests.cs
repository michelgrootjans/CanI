using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using CanI.Core;
using NUnit.Framework;

namespace CanI.Tests
{
    [TestFixture]
    public class StringAbiltyTests
    {
        [Test]
        public void a_null_abiltiy_doesnt_allow_anything()
        {
            Then.IShouldNotBeAbleTo("view", "index");
        }

        [Test]
        public void a_simple_ability_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "index"));
            Then.IShouldBeAbleTo("view", "index");
        }
}
}
