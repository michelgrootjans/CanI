using CanI.Core.Configuration;
using CanI.Tests.TestUtilities;
using NUnit.Framework;

namespace CanI.Tests.Abilities
{
    [TestFixture]
    public class PostFixAbiltyTest
    {

        [SetUp]
        public void SetUp()
        {
            AbilityConfiguration.Reset();
        }

        private class CustomerViewModel { }
        [Test]
        public void abilities_ignore_postfixes_by_default()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("edit").On("customer"));
            Then.IShouldBeAbleTo("edit", new CustomerViewModel());
        }

        private class EditCustomer { }
        [Test]
        public void abilities_ignore_prefixes_by_default()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("edit").On("customer"));
            Then.IShouldBeAbleTo("edit", new EditCustomer());
        }

        private class UpdateCustomer { }
        [Test]
        public void abilities_ignore_prefixes_aliases_by_default()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("edit").On("customer"));
            Then.IShouldBeAbleTo("edit", new UpdateCustomer());
        }

        private class EditCustomerViewModel { }
        [Test]
        public void abilities_ignore_prefixes_and_postfixes_by_default()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("edit").On("customer"));
            Then.IShouldBeAbleTo("edit", new EditCustomerViewModel());
        }

        private class ClientDto { }
        [Test]
        public void abilities_ignore_postfixes_with_subjectAlieas()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("edit").On("customer");
                c.ConfigureSubjectAliases("customer", "client");
            });
            Then.IShouldBeAbleTo("edit", new ClientDto());
        }

        private class GetClientDetailDto { }
        [Test]
        public void abilities_ignore_prefixes_and_postfixes_by_default_with_subjectAlieas()
        {
            AbilityConfiguration.ConfigureWith(c =>
            {
                c.Allow("edit").On("customer");
                c.ConfigureSubjectAliases("customer", "client");
            });
            Then.IShouldBeAbleTo("edit", new GetClientDetailDto());
        }

        private class CustomerEditViewModel { }
        [Test]
        public void abilities_ignore_reversed_prefixes_and_postfixes_by_default()
        {
            AbilityConfiguration.ConfigureWith(c => c.Allow("edit").On("customer"));
            Then.IShouldBeAbleTo("edit", new CustomerEditViewModel());
        }

    }
}