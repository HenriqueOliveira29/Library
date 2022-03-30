using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models.Auth;
using Supermarket.Data.Models.Helper;

namespace Supermarket.API.Controllers
{
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<MessageHelper> ForgotPassword(ForgotPasswordDTO dto)
        {
            return await _userService.SendEmailForgotPassword(dto);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<MessageHelper> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            return await _userService.ResetPassword(resetPasswordDTO);
        }


    }
}
