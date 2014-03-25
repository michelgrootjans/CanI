using System.Collections.Generic;
using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Aliasing
{
    [TestFixture]
    public class CustomSubjectAliasAbiltyTests
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void an_ability_can_be_checked_with_a_subject_alias()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureSubjectAliases("customer", "customers");
            });
            Then.IShouldBeAbleTo("view", "customer");
            Then.IShouldBeAbleTo("view", "customers");
        }

        [Test]
        public void subject_alias_is_case_insensitive()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureSubjectAliases("cusTomEr", "cuStoMErs");
            });
            Then.IShouldBeAbleTo("view", "cUstoMer");
            Then.IShouldBeAbleTo("view", "cusTOmers");
        }

        [Test]
        public void subject_alias_is_logged_in_verbose_mode()
        {
            var messages = new List<string>();
            AbilityConfiguration.Debug(messages.Add).Verbose();
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureSubjectAliases("customer", "customers");
            });
            Then.IShouldBeAbleTo("view", "customers");

            messages.ShouldContain("creating subject alias 'customers = customer'");
        }

        [Test]
        public void redundant_subject_alias_doesnt_crash()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("customer");
                c.ConfigureSubjectAliases("customer", "customers");
                c.ConfigureSubjectAliases("customer", "customers");
            });
            Then.IShouldBeAbleTo("view", "customer");
            Then.IShouldBeAbleTo("view", "customers");
        }

        [Test]
        public void overwrite_subject_alias_applies_the_new_alias()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("client");
                c.ConfigureSubjectAliases("customer", "customers");
                c.ConfigureSubjectAliases("client", "customers");
            });
            Then.IShouldBeAbleTo("view", "client");
            Then.IShouldBeAbleTo("view", "customers");
            Then.IShouldNotBeAbleTo("view", "customer");
        }

        [Test]
        public void overwrite_subject_alias_logs_in_verbose_mode()
        {
            var messages = new List<string>();
            AbilityConfiguration.Debug(messages.Add).Verbose();
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("view").On("client");
                c.ConfigureSubjectAliases("customer", "customers");
                c.ConfigureSubjectAliases("client", "customers");
            });
            Then.IShouldBeAbleTo("view", "client");

            messages.ShouldContain("overwriting subject alias 'customers = client' (was 'customers = customer')");
        }

    }
}