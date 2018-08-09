using Microsoft.AspNetCore.Identity;

namespace Intervue.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BlobContainerID { get; set; }
    }

    public static class ApplicationUserRoles
    {
        public const string Member = "Member";
        public const string Admin = "Admin";
    }
}