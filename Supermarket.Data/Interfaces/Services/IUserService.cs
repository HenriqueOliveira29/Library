using Supermarket.Data.Models.Auth;
using Supermarket.Data.Models.Helper;
using Supermarket.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Interfaces.Services
{
    public interface IUserService
    {
        Task<MessageHelper> SendEmailForgotPassword(ForgotPasswordDTO dto);

        Task<MessageHelper> ResetPassword(ResetPasswordDTO reser);

        Task<MessageHelper<UserDTO>> GetById(string id, string currentUserId);

        Task<MessageHelper> UpdatePassword(string id, PasswordChangeDTO passwordChange, string currentUserId);

    }
}
