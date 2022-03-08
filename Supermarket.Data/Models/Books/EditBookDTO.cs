using FluentValidation;
using Supermarket.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Books
{
    public class EditBookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int StockNumber { get; set; }

        public int AuthorId { get; set; }

        public Book ToEntity()
        {
            return new Book
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Description = Description,
                StockNumber = StockNumber,
                AuthorId = AuthorId,

            };
        }
    }

    public class EditBookDTOValidator : AbstractValidator<EditBookDTO> {

        public EditBookDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome do livro deve estar preenchido!");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Preco deve estar preenchido!");
                

            RuleFor(x => x.Description).NotEmpty().WithMessage("Descricao deve estar preenchida!");

            RuleFor(x => x.StockNumber).NotEmpty().WithMessage("Numero de stock deve estar preenchido!");

            RuleFor(x => x.AuthorId).GreaterThanOrEqualTo(0).WithMessage("Nº de autor deve estar preenchido");

        }
    }
}
