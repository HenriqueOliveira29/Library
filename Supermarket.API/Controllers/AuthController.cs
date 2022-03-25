using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models.Auth;
using Supermarket.Data.Models.Helper;

namespace Supermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            _authService = authService;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<MessageHelper<AuthDTO>> Login([FromBody] LoginDTO login)
        {
            return await _authService.Login(login);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterDTO authDTO) {
            MessageHelper result = new();

            try
            {
               RegisterDTOValidator validator = new RegisterDTOValidator();
                var responseValidatorDTO = await validator.ValidateAsync(authDTO);
                if (responseValidatorDTO.IsValid == false) {
                    result.Message = responseValidatorDTO.Errors.FirstOrDefault()!.ErrorMessage;
                    return Ok(result);
                }

                var passwordValidator = new PasswordValidator<ApplicationUser>();
                var resultValidatePassword = await passwordValidator.ValidateAsync(_userManager, null!, authDTO.Password);
                if (!resultValidatePassword.Succeeded) {
                    result.Sucess = false;
                    result.Message = "A palavra passe nao cumpre os requisitos";
                    return Ok(result);
                }


                ApplicationUser userInDatabase = await _userManager.FindByEmailAsync(authDTO.Email);
                if (userInDatabase == null)
                {
                    userInDatabase = new ApplicationUser
                    {
                        Email = authDTO.Email,
                        EmailConfirmed = true,
                        UserName = authDTO.Email,
                        Name = authDTO.Username!,
                    };
                    var resultCreateUserInDatabase = await _userManager.CreateAsync(userInDatabase, authDTO.Password);
                    if (resultCreateUserInDatabase.Succeeded)
                    {
                        result.Sucess = true;
                        result.Message = "Utilizador criado com sucesso";
                    }
                    else
                    {
                        result.Sucess = false;
                        result.Message = "Ocorreu um erro a criar o utilizador!";
                    }
                }

                else {
                    result.Sucess = false;
                    result.Message = "Já existe um utilizador com esse email registado";
                }
            }
            catch (Exception ex) {
                result.Sucess = false;
                result.Message = "Ocorreu um erro inesperado ao criar o utilizador.";
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult Logout() {
            MessageHelper<object> response = new MessageHelper<object>();

            try
            {
                HttpContext.Session.Clear();
                response.Sucess = true;
                response.Message = "";

            }
            catch (Exception ex) {
                response.Sucess= false;
                response.obj = null;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetUser() {
            MessageHelper<AuthDTO> response = new MessageHelper<AuthDTO>();

            try
            {
                response.Sucess = true;
                response.Message = "";
                response.obj = new AuthDTO()
                {
                    Token = HttpContext.Session.GetString("token") ?? null,
                    UserName = HttpContext.Session.GetString("username") ?? null,
                    Id = HttpContext.Session.GetString("id") ?? null,
                    Roles = HttpContext.Session.GetString("roles")?.Split(",") ?? null,
                };
            }
            catch (Exception ex) { 
                response.Sucess= false;
                response.Message= ex.Message;
                response.obj = null;
            }

            return Ok(response);
        }

    }
}
