using CanI.Core;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Context
{
    [TestFixture]
    public class ExternalContextAbilityTest
    {
        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        [Test]
        public void with_positive_context_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c => 
                c.AllowTo("edit", "order")
                 .If(() => true)
                 );
            Then.IShouldBeAbleTo("edit", "order");
        }

        [Test]
        public void with_negative_context_denies_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.AllowTo("edit", "order")
                 .If(() => false)
                 );
            Then.IShouldNotBeAbleTo("edit", "order");
        }

        [Test]
        public void with_positive_subject_context_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.AllowTo("edit", "order")
                 .If<Order>(o => o.IsPending)
                );
            Then.IShouldBeAbleTo("edit", new Order{ IsPending = true });
        }

        [Test]
        public void with_negative_subject_context_allows_its_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.AllowTo("edit", "order")
                 .If<Order>(o => o.IsPending)
                );
            Then.IShouldNotBeAbleTo("edit", new Order{IsPending = false});
        }

        [Test]
        public void with_subject_context_still_allows_string_action()
        {
            AbilityConfiguration.ConfigureWith(c =>
                c.AllowTo("edit", "order")
                 .If<Order>(o => o.IsPending)
                );
            Then.IShouldBeAbleTo("edit", "order");
        }

        private class Order
        {
            public bool IsPending { get; set; }
        }
    }

}