using Microsoft.AspNetCore.Identity;

namespace Smeat.Leader.Infrastructure.Identity
{
    public class LeaderUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
