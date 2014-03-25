using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Abilities
{
    [TestFixture]
    public class StringAbiltyTests
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void a_null_abiltiy_doesnt_allow_anything()
        {
            Then.IShouldNotBeAbleTo("view", "customer");
        }

        [Test]
        public void a_simple_ability_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("customer"));
            Then.IShouldBeAbleTo("view", "customer");
        }

        [Test]
        public void abilities_do_not_cross()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("create").On("customer");
                c.Allow("edit").On("order");
            });
            Then.IShouldBeAbleTo("create", "customer");
            Then.IShouldBeAbleTo("edit", "order");

            Then.IShouldNotBeAbleTo("edit", "customer");
            Then.IShouldNotBeAbleTo("create", "order");
        }

        [Test]
        public void allow_all__allows_anything_on_the_subject()
        {
            AbilityConfiguration.ConfigureWith(c => c.AllowAnything().On("customer"));
            Then.IShouldBeAbleTo("view", "customer");
            Then.IShouldBeAbleTo("create", "customer");
            Then.IShouldBeAbleTo("edit", "customer");
            Then.IShouldBeAbleTo("delete", "customer");
            Then.IShouldBeAbleTo("discombobulate", "customer");
            Then.IShouldNotBeAbleTo("view", "order");
        }

        [Test]
        public void allow_all_on_anything__allows_everything()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").OnEverything());
            Then.IShouldBeAbleTo("view", "customer");
            Then.IShouldBeAbleTo("view", "order");
            Then.IShouldNotBeAbleTo("edit", "customer");
        }

        [Test]
        public void postfix_on_action_is_not_allowed()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("customer"));
            Then.IShouldNotBeAbleTo("view_special", "order");
        }

        [Test]
        public void redundant_configuration_doesnt_crash()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("customer"));
            AbilityConfiguration.ConfigureWith(c => c.Allow("view").On("customer"));
            Then.IShouldBeAbleTo("view", "customer");
        }
    }
}
