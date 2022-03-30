using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Auth
{
    public class ResetPasswordDTO
    {
        public string Password { get; set; }
        public string Token { get; set; }
        public string UserId{ get; set; }
    }
}
