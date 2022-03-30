using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models.Auth;
using Supermarket.Data.Models.Helper;
using Supermarket.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Library.DAL.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        private readonly List<String> INTERNAL_ROLES = new()
        {
            Roles.Admin.Value
        };

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IEmailService emailService, IConfiguration config, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _config = config;
            userRepository = userRepository;
        }

        public Task<MessageHelper<UserDTO>> GetById(string id, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageHelper> ResetPassword(ResetPasswordDTO reset)
        {
            MessageHelper result = new MessageHelper();

            try
            {
                var user = await _userManager.FindByIdAsync(reset.UserId);

                if(user == null)
                {
                    result.Sucess = false;
                    result.Message = "Não encontramos este utilizador";
                    return result;
                }

                var resetPassword = await _userManager.ResetPasswordAsync(user, reset.Token, reset.Password);

                if (!resetPassword.Succeeded)
                {
                    string errors = string.Empty;
                    foreach (var error in resetPassword.Errors)
                    {
                        errors += error.Description + "\r";
                    }

                    result.Sucess = false;
                    result.Message= "Ocorreu um erro ao redefinir a password";
                    return result;
                }
                result.Sucess=true;
                result.Message = "Password redefinida com sucesso";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = "Ocorreu um erro inesperado ao redefinir a password";

            }
            return result;
        }

        public async Task<MessageHelper> SendEmailForgotPassword(ForgotPasswordDTO dto)
        {
            MessageHelper result = new MessageHelper();

            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if(user == null)
                {
                    result.Sucess = false;
                    result.Message = "Este email nao existe";
                    return result;
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                string frontendUrl = _config["FrontendUrl"];
                token = HttpUtility.UrlEncode(token);
                string link = $"{frontendUrl}/ResetPassword?userId={user.Id}&token={token}";

                RecoveryPasswordDTO obj = new()
                {
                    Link = link
                };

                MessageHelper resultSendEmail = await _emailService.SendEmail(dto.Email,"RecoverPassword", obj, "Library" );

                if (resultSendEmail.Sucess == false)
                {
                    result.Sucess = false;
                    result.Message = "Email não foi enviado";
                }
                result.Sucess = true;
                result.Message = "Email enviado com sucesso";

                

            }catch (Exception ex)
            {
                result.Sucess=false;
                result.Message = "Não foi possivel enviar o email";
            }

            return result;
        }

        public Task<MessageHelper> UpdatePassword(string id, PasswordChangeDTO passwordChange, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }

}
