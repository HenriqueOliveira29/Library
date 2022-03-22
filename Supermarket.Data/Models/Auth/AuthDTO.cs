using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Auth
{
    public class AuthDTO
    {
        public string UserName { get; set; }

        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string[] Roles { get; set; }
    }
}
