using Microsoft.AspNetCore.Identity;

namespace Smeat.Leader.Web.Areas.Identity.Entities
{
    public class LeaderUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}