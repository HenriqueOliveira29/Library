using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models.Auth;
using Supermarket.Data.Models.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IOptions<IdentityOptions> _optionsAccessor;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config,
            IPasswordHasher<ApplicationUser> passwordHasher, IHttpContextAccessor httpContextAccessor,
            IOptions<IdentityOptions> optionsAccessor)
        {
            _userManager = userManager;
            _config = config;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
            _optionsAccessor = optionsAccessor;
        }

        public async Task<MessageHelper<AuthDTO>> Login(LoginDTO login)
        {
            MessageHelper<AuthDTO> response = new MessageHelper<AuthDTO>();
            try
            {
                var user = await AuthenticateUser(login);

                var userRoles = (await _userManager.GetRolesAsync(user)).ToList();

                //if (userRoles == null || userRoles.Count() == 0)
                //{
                    //response.Sucess = false;
                    //response.Message = "Utilizador com configuracao invalida";
                    //return response;
                //}

                if (user != null)
                {
                    var tokenString = await GenerateJSONWebToken(user, userRoles);

                    AuthDTO responseObj = new AuthDTO()
                    {
                        Id = user.Id,
                        Token = tokenString,
                        UserName = user.Name,
                        Roles = userRoles.ToArray(),

                    };

                    SaveToken(responseObj);
                    response.Sucess = true;
                    response.obj = responseObj;
                }
                else {
                    response.Sucess = false;
                    response.Message = "Os dados estão incorretos.";
                }
                
            }
            catch (Exception ex) {
                response.Sucess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private async Task<ApplicationUser> AuthenticateUser(LoginDTO login) 
        {
            ApplicationUser? user = null;
            try
            {
                //Se o utilizador existe
                var userfound = await _userManager.FindByEmailAsync(login.Email);

                //se existir
                if (userfound != null) {
                    if (_passwordHasher.VerifyHashedPassword(userfound, userfound.PasswordHash, login.Password) == PasswordVerificationResult.Success)
                    {
                        user = userfound;

                        user.AccessFailedCount = 0;
                        user.LockoutEnd = null;

                        await _userManager.UpdateAsync(user);
                    }
                    else 
                    {
                        await UserAccessFailed(userfound);
                    }
                }
                return user;
            }
            catch (Exception ex) {
                return user;
            }
        
        }

        private async Task UserAccessFailed(ApplicationUser user) {
        
            user.AccessFailedCount++;
            await _userManager.UpdateAsync(user);
        }

        private async Task<string> GenerateJSONWebToken(ApplicationUser userInfo, List<string> userRoles) {
            var user = await _userManager.FindByIdAsync(userInfo.Id);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = GetTokenClaims(user).Union(userClaims).ToList();

            foreach (var role in userRoles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"], claims: claims, signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static IEnumerable<Claim> GetTokenClaims(IdentityUser user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
        }

        public virtual MessageHelper<bool> SaveToken(AuthDTO user) {
            MessageHelper<bool> response = new();

            try
            {
                _httpContextAccessor.HttpContext.Session.SetString("token", user.Token);
                _httpContextAccessor.HttpContext.Session.SetString("username", user.UserName);
                _httpContextAccessor.HttpContext.Session.SetString("id", user.Id);
                _httpContextAccessor.HttpContext.Session.SetString("roles", String.Join(",", user.Roles));

                response.Sucess = true;
                response.obj = true;
            }
            catch (Exception ex) {
                response.Sucess = false;
                response.obj = false;
                response.Message = ex.Message;

            }

            return response;
        }
    }
}
