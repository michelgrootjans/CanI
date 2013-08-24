using CanI.Core;
using NUnit.Framework;

namespace CanI.Tests
{
    [TestFixture]
    public class PostFixAbiltyTest
    {
        private class CustomerViewModel
        {
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
                c.IgnoreSubjectPostfix("ViewModel");
            });

            Then.IShouldBeAbleTo("view", new CustomerViewModel());
        }

        [Test]
        public void abilities_ignore_postfixes_case_insensitive()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("view", "customer");
                c.IgnoreSubjectPostfix("viewmodel");
            });

            Then.IShouldBeAbleTo("view", new CustomerViewModel());
        }

    }
}