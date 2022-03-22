using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Identity;

namespace Supermarket.Data.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public sealed class Roles : SmartEnum<Roles, string> {
        public static readonly Roles Admin = new Roles("Admin", "Admin");
        public static readonly Roles Client = new Roles("Client", "Client");
        protected Roles(string name, string value) : base(name, value) 
        {
        }
    }
}
