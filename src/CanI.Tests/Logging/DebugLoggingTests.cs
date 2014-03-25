using System.Collections.Generic;
using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Logging
{
    [TestFixture]
    public class DebugLoggingTests
    {
        private List<string> debugMessages;

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
            debugMessages = new List<string>();
        }

        [Test]
        public void no_logging_happens_when_ability_is_not_checked()
        {
            AbilityConfiguration.Debug(t => debugMessages.Add(t));

            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("order"));

            debugMessages.ShouldNotContain("user has the ability to view/order")
                         .ShouldNotContain("user can view/order");
        }

        [Test]
        public void checking_ability_should_log_it()
        {
            AbilityConfiguration.Debug(t => debugMessages.Add(t));

            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("order"));
            Then.IShouldBeAbleTo("view", "order");

            debugMessages.ShouldNotContain("user has the ability to view/order")
                         .ShouldContain("user can view/order");
        }

        [Test]
        public void checking_denied_ability_should_log_it()
        {
            AbilityConfiguration.Debug(t => debugMessages.Add(t));

            Then.IShouldNotBeAbleTo("view", "order");

            debugMessages.ShouldContain("user cannot view/order");
        }

        [Test]
        public void checking_ability_with_verbosity_should_log_it()
        {
            AbilityConfiguration.Debug(t => debugMessages.Add(t)).Verbose();

            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("order"));
            Then.IShouldBeAbleTo("view", "order");

            debugMessages.ShouldContain("user has the ability to view/order")
                         .ShouldContain("user can view/order");
        }

    }
}