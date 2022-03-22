using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Entities
{
    public class ApplicationUser: IdentityUser
    {   
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }


    }
}
