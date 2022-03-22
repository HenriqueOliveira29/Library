using Supermarket.Data.Models.Auth;
using Supermarket.Data.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Interfaces.Services
{
    public interface IAuthService
    {
        Task<MessageHelper<AuthDTO>> Login(LoginDTO login);
    }
}
