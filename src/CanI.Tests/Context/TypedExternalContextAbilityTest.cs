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
                c.Allow("edit").On<Order>(o => o.IsPending)
                );

            Then.IShouldBeAbleTo("edit", new Order { IsPending = true });
        }

        [Test]
        public void with_negative_subject_context_denies_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow("edit").On<Order>(o => o.IsPending)
                );
            Then.IShouldNotBeAbleTo("edit", new Order { IsPending = false });
        }

        [Test]
        public void with_negative_subject_context_with_full_access_still_denies_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.AllowAnything().On<Order>(o => o.IsPending)
                );
            Then.IShouldNotBeAbleTo("edit", new Order { IsPending = false });
        }

        [Test]
        public void with_subject_context_still_allows_string_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.Allow("edit").On<Order>(o => o.IsPending)
                );
            Then.IShouldNotBeAbleTo("edit", "blah");
            Then.IShouldBeAbleTo("edit", "order");
        }

//        [Test]
//        public void with_double_configuration_works_as_expected()
//        {
//            AbilityConfiguration.ConfigureWith(c =>
//                c.Allow("edit").On<Order>(o => o.IsPending)
//                               .On<OrderDto>(o => o.IsPending)
//            );

//            Then.IShouldBeAbleTo("edit", new Order { IsPending = true });
//            Then.IShouldNotBeAbleTo("edit", new Order { IsPending = false });
//            Then.IShouldBeAbleTo("edit", new OrderDto { IsPending = true });
//            Then.IShouldNotBeAbleTo("edit", new OrderDto { IsPending = false });
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