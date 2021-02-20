using Xunit;

namespace Smeat.Leader.UnitTests.Leader.Core.Customer
{
    public class CustomerIsValid
    {
        const long dealerId = 1;

        [Fact]
        public void CreateValidCustomer()
        {
            var customer = new Smeat.Leader.Core.Entities.CustomerAggregate.Customer(dealerId);
            customer.FirstName = "Peter";

            Assert.Equal("Peter", customer.FirstName);
        }
    }
}
