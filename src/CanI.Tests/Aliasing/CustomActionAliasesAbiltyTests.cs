using System.Collections.Generic;
using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Aliasing
{
    [TestFixture]
    public class CustomActionAliasesAbiltyTests
    {
        [Test]
        public void unconfigured_alias_doesnt_work()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("customer"));
            Then.IShouldNotBeAbleTo("consult", "customer");
        }

        [Test]
        public void configured_alias_works()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureActionAliases("view", "consult");
            });
            Then.IShouldBeAbleTo("consult", "customer");
        }

        [Test]
        public void configured_alias_is_case_insensitive()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureActionAliases("view", "Consult");
            });
            Then.IShouldBeAbleTo("consult", "customer");
        }

        [Test]
        public void redundant_configured_alias_doesnt_crash()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureActionAliases("view", "consult");
                c.ConfigureActionAliases("view", "consult");
            });
            Then.IShouldBeAbleTo("consult", "customer");
        }

        [Test]
        public void overwriting_alias_configures_the_last_one()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("update").On("customer");
                c.ConfigureActionAliases("view", "consult");
                c.ConfigureActionAliases("edit", "consult");
            });
            Then.IShouldBeAbleTo("update", "customer");
            Then.IShouldBeAbleTo("consult", "customer");
        }

        [Test]
        public void configured_alias_doesnt_log()
        {
            var debugMessages = new List<string>();
            AbilityConfiguration.Debug(debugMessages.Add);
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureActionAliases("view", "consult");
            });
            Then.IShouldBeAbleTo("consult", "customer");
            debugMessages.ShouldNotContain(string.Format("creating action alias 'consult = view'"));
        }

        [Test]
        public void configured_alias_logs_verbose()
        {
            var debugMessages = new List<string>();
            AbilityConfiguration.Debug(debugMessages.Add).Verbose();
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureActionAliases("view", "consult");
            });
            Then.IShouldBeAbleTo("consult", "customer");
            debugMessages.ShouldContain(string.Format("creating action alias 'consult = view'"));
        }

        [Test]
        public void overwriting_alias_logs_the_change_on_verbose()
        {
            var debugMessages = new List<string>();
            AbilityConfiguration.Debug(debugMessages.Add).Verbose();
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("update").On("customer");
                c.ConfigureActionAliases("view", "consult");
                c.ConfigureActionAliases("edit", "consult");
            });
            Then.IShouldBeAbleTo("consult", "customer");
            debugMessages.ShouldContain(string.Format("overwriting action alias 'consult = edit' (was 'consult = view')"));
        }
    }
}