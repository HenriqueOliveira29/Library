using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.User
{
    public class PasswordChangeDTO
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
