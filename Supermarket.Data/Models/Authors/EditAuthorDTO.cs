using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Authors
{
    public class EditAuthorDTO
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeadDate { get; set; }
    }

    public class EditAuthorDTOValidator : AbstractValidator<EditAuthorDTO> {

        public EditAuthorDTOValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Este campo nao pode estar vazio");

            RuleFor(x => x.Name).NotEmpty().WithMessage("O Author tem que ter um nome");

            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("O Author tem que ter uma data de nascimento");

            When(x => x.DeadDate != null, () =>
            {
                RuleFor(x => x.DeadDate).NotNull().WithMessage("data de morte invalida").GreaterThan(x => x.BirthDate.AddYears(10)).WithMessage("A data de morte tem que ser no minimo 10 anos mais velha que a data de morte");
            });
        }
    }
}
