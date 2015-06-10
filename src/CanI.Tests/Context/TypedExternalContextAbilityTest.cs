using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Context
{
    [TestFixture]
    public class TypedExternalContextAbilityTest
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void with_positive_subject_context_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow("view").On<Order>(o => o.IsPending)
                );

            Then.IShouldBeAbleTo("view", new Order { IsPending = true });
        }

        [Test]
        public void with_negative_subject_context_denies_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow("view").On<Order>(o => o.IsPending)
                );
            Then.IShouldNotBeAbleTo("view", new Order { IsPending = false });
        }

        [Test]
        public void with_negative_subject_context_with_full_access_still_denies_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.AllowAnything().On<Order>(o => o.IsPending)
                );
            Then.IShouldNotBeAbleTo("view", new Order { IsPending = false });
        }

        [Test]
        public void with_subject_context_doesnt_allow_string_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow("view").On<Order>(o => o.IsPending)
                );
            Then.IShouldNotBeAbleTo("view", "order");
            Then.IShouldNotBeAbleTo("view", "blah");
        }

        [Test]
        public void doesn_allow_LIKE_subjects()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow("view").On<Order>(o => o.IsPending)
                );
            Then.IShouldNotBeAbleTo("view", new OrderDto { IsPending = true });
        }

        [TestCase("view", "view")]
        [TestCase("view", "show")]
        [TestCase("view", "read")]
        [TestCase("view", "index")]
        [TestCase("view", "detail")]

        [TestCase("create", "create")]
        [TestCase("create", "insert")]
        
        [TestCase("edit", "edit")]
        [TestCase("edit", "update")]
        [TestCase("edit", "change")]

        [TestCase("delete", "delete")]
        [TestCase("delete", "remove")]
        [TestCase("delete", "destroy")]
        public void allows_action_synonyms(string configuredAction, string testedAction)
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow(configuredAction).On<Order>(o => o.IsPending)
                );

            Then.IShouldBeAbleTo(testedAction, new Order { IsPending = true });
            Then.IShouldNotBeAbleTo(testedAction, new Order { IsPending = false });
        }

        [Test]
        public void denies_Edit_when_CanEdit_is_false()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow("edit").On<Order>(o => true)
            );

            Then.IShouldNotBeAbleTo("edit", new Order { CanEdit = false });
            Then.IShouldNotBeAbleTo("update", new Order { CanEdit = false });
            Then.IShouldNotBeAbleTo("change", new Order { CanEdit = false });
        }

        private class Order
        {
            public Order()
            {
                CanEdit = true;
            }

            public bool IsPending { get; set; }
            public bool CanEdit { get; set; }
        }

        private class OrderDto
        {
            public bool IsPending { get; set; }
        }
    }

}