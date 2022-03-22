using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Auth
{
    public class RegisterDTO
    {
        public string? Username { get; set; }
        public string? Email   { get; set; }
        public string? Password { get; set; }
    }

    public class RegisterDTOValidator : AbstractValidator<RegisterDTO> 
    {
        public RegisterDTOValidator() {
            RuleFor(x => x.Username).NotNull().WithMessage("Insira o username")
            .NotEmpty().WithMessage("Insira o username");

            RuleFor(x => x.Email).NotNull().WithMessage("Insira o Email")
            .NotEmpty().WithMessage("Insira o Email");

            RuleFor(x => x.Password).NotNull().WithMessage("Insira a password")
            .NotEmpty().WithMessage("Insira a password");
        }
    }
}
