using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Smeat.Leader.SharedKernel.Identity
{
    public class LeaderUser : IdentityUser<long>
    {
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }
    }
}
