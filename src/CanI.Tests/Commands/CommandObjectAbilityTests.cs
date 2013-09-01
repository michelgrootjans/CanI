using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Commands
{
    [TestFixture]
    public class CommandObjectAbilityTests
    {
        private class EditOrderCommand { }
        private class UpdateOrderCommand { }

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void denies_commands_by_default()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowTo("edit", "order"));
            Then.IShouldNotBeAbleToExecute(new EditOrderCommand());
        }

        [Test]
        public void allows_command_by_convention()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("edit", "order");
                c.ConfigureCommandConvention("{action}{subject}Command");
            });
            Then.IShouldBeAbleToExecute(new EditOrderCommand());
        }

        [Test]
        public void allows_command_by_convention_with_action_alias()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("edit", "order");
                c.ConfigureCommandConvention("{action}{subject}Command");
            });
            Then.IShouldBeAbleToExecute(new UpdateOrderCommand());
        }

        [Test]
        public void allows_command_by_convention_for_manage_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("manage", "order");
                c.ConfigureCommandConvention("{action}{subject}Command");
            });
            Then.IShouldBeAbleToExecute(new EditOrderCommand());
        }

        [Test]
        public void allows_command_by_convention_for_edit_all_subjects()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("edit", "all");
                c.ConfigureCommandConvention("{action}{subject}Command");
            });
            Then.IShouldBeAbleToExecute(new EditOrderCommand());
        }

        [Test]
        public void allows_command_by_convention_for_manage_all_subjects()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.AllowTo("manage", "all");
                c.ConfigureCommandConvention("{action}{subject}Command");
            });
            Then.IShouldBeAbleToExecute(new EditOrderCommand());
        }

   }

}