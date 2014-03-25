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
        public void checking_ability_should_log_it()
        {
            AbilityConfiguration.Debug(t => debugMessages.Add(t));
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("order"));
            Then.IShouldBeAbleTo("view", "order");

            debugMessages.ShouldContain("user can view/order");
        }

    }
}