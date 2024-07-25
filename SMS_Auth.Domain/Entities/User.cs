
using Microsoft.AspNetCore.Identity;

namespace SMS_Auth.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
