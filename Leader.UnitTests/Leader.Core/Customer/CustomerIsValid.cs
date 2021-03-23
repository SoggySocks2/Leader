using Xunit;

namespace Smeat.Leader.UnitTests.Leader.Core.Customer
{
    public class CustomerIsValid
    {
        const long GROUP_ID = 1;

        [Fact]
        public void CreateValidCustomer()
        {
            var customer = new Smeat.Leader.Core.Entities.Customer(GROUP_ID);
            customer.FirstName = "Peter";

            Assert.Equal("Peter", customer.FirstName);
        }
    }
}
