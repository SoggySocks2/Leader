using Smeat.Leader.Core.Entities.CustomerAggregate;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Smeat.Leader.Infrastructure.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _dbContext;

        public CustomerRepository (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Customer>> GetByName(string name, int skip, int take)
        {
            return await _dbContext.Customers
                .Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name))
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetByName_CountAsync(string name)
        {
            return await _dbContext.Customers.Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name)).CountAsync();
        }
    }
}
