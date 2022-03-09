using FluentValidation;
using Supermarket.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Authors
{
    public class CreateAuthorDTO
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeadDate { get; set; }

        public Author ToEntity() {
            return new Author
            {
                Name = Name,
                BirthDate = BirthDate,
                DeadDate = DeadDate      
            };
        
        }
    }

    public class CreateAuthorDTOValidator : AbstractValidator<CreateAuthorDTO>
    {
        public CreateAuthorDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("O campo nome nao pode estar vazio");

            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("O campo data de nascimento nao pode estar vazio");

            When(x => x.DeadDate != null, () =>
            {
                RuleFor(x => x.DeadDate).NotNull().WithMessage("data de morte invalida").GreaterThan(x => x.BirthDate.AddYears(10)).WithMessage("A data de morte tem que ser no minimo 10 anos mais velha que a data de morte");
            });
        }
    }
}
