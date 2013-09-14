﻿using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Commands
{
    [TestFixture]
    public class CommandObjectAbilityTests
    {
        private class EditOrder { }
        private class EditOrderCommand { }
        private class UpdateOrderCommand { }

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void denies_command_by_default_convention()
        {
            Then.IShouldNotBeAbleToExecute(new EditOrderCommand());
        }

        [Test]
        public void allows_simple_command_objects_by_default()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("edit", "order"));
            Then.IShouldBeAbleToExecute(new EditOrder());
        }

        [Test]
        public void allows_command_by_convention()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("edit", "order"));
            Then.IShouldBeAbleToExecute(new EditOrderCommand());
        }

        [Test]
        public void allows_command_by_convention_with_action_alias()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("edit", "order"));
            Then.IShouldBeAbleToExecute(new UpdateOrderCommand());
        }

        [Test]
        public void allows_command_by_convention_for_manage_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("manage", "order"));
            Then.IShouldBeAbleToExecute(new EditOrderCommand());
        }

        [Test]
        public void allows_command_by_convention_for_edit_all_subjects()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("edit", "all"));
            Then.IShouldBeAbleToExecute(new EditOrderCommand());
        }

        [Test]
        public void allows_command_by_convention_for_manage_all_subjects()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("manage", "all"));
            Then.IShouldBeAbleToExecute(new EditOrderCommand());
        }

   }

}