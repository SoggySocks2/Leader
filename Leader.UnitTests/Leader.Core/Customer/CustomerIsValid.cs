using Xunit;

namespace Smeat.Leader.UnitTests.Leader.Core.Customer
{
    public class CustomerIsValid
    {
        [Fact]
        public void CreateValidCustomer()
        {
            var customer = new Smeat.Leader.Core.Entities.CustomerAggregate.Customer();
            customer.FirstName = "Peter";

            Assert.Equal("Peterz", customer.FirstName);
        }
    }
}
