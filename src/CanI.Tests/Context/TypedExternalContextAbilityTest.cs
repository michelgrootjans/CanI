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

        [Test]
        public void allows_action_synonyms()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow("view").On<Order>(o => o.IsPending)
                );

            Then.IShouldBeAbleTo("index", new Order { IsPending = true });
            Then.IShouldBeAbleTo("show", new Order { IsPending = true });
            Then.IShouldBeAbleTo("read", new Order { IsPending = true });

            Then.IShouldNotBeAbleTo("index", new Order { IsPending = false });
            Then.IShouldNotBeAbleTo("show", new Order { IsPending = false });
            Then.IShouldNotBeAbleTo("read", new Order { IsPending = false });
        }

        //        [Test]
        //        public void with_double_configuration_works_as_expected()
        //        {
        //            AbilityConfiguration.ConfigureWith(c =>
        //                c.Allow("view").On<Order>(o => o.IsPending)
        //                               .On<OrderDto>(o => o.IsPending)
        //            );

        //            Then.IShouldBeAbleTo("view", new Order { IsPending = true });
        //            Then.IShouldNotBeAbleTo("view", new Order { IsPending = false });
        //            Then.IShouldBeAbleTo("view", new OrderDto { IsPending = true });
        //            Then.IShouldNotBeAbleTo("view", new OrderDto { IsPending = false });
        //        }

        private class Order
        {
            public bool IsPending { get; set; }
        }

        private class OrderDto
        {
            public bool IsPending { get; set; }
        }
    }

}