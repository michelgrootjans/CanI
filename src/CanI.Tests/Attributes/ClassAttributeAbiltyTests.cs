﻿using CanI.Core.Authorization;
using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Attributes
{
    [TestFixture]
    public class ClassAttributeAbiltyTests
    {
        private class PromoteCustomerCommand { }
        private class PromoteCustomerToGoldCommand { }

        [AuthorizeIfICan("promoteToGold", "Customer")]
        private class PromoteCustomerToGoldCommand_WithAttribute { }

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void a_null_abiltiy_doesnt_allow_anything()
        {
            Then.IShouldNotBeAbleToExecute(new PromoteCustomerCommand());
        }

        [Test]
        public void following_the_convention_allows_an_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("promote", "customer"));
            Then.IShouldBeAbleToExecute(new PromoteCustomerCommand());
        }

        [Test]
        public void not_following_the_convention_denies_its_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("promoteToGold", "customer"));
            Then.IShouldNotBeAbleToExecute(new PromoteCustomerToGoldCommand());
        }

        [Test]
        public void not_following_the_convention_with_an_attribute_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("promoteToGold", "customer"));
            Then.IShouldBeAbleToExecute(new PromoteCustomerToGoldCommand_WithAttribute());
        }

    }
}