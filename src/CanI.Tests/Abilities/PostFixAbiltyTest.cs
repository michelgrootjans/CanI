using CanI.Core;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Abilities
{
    [TestFixture]
    public class PostFixAbiltyTest
    {
        private class CustomerViewModel
        {
        }

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void abilities_dont_ignore_postfixes_by_default()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("view", "customer"));

            Then.IShouldNotBeAbleTo("view", new CustomerViewModel());
        }

        [Test]
        public void abilities_ignore_postfixes()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("view", "customer");
                c.IgnoreSubjectPostfixes("ViewModel");
            });

            Then.IShouldBeAbleTo("view", new CustomerViewModel());
        }

        [Test]
        public void abilities_ignore_postfixes_case_insensitive()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("view", "customer");
                c.IgnoreSubjectPostfixes("viewmodel");
            });

            Then.IShouldBeAbleTo("view", new CustomerViewModel());
        }

    }
}