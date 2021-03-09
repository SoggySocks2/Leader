using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smeat.Leader.Infrastructure.Data
{
    public interface ICustomerRepository
    {
        Task<List<Core.Entities.CustomerAggregate.Customer>> GetByName(string name, int skip, int take);
        Task<int> GetByName_CountAsync(string name);
    }
}
