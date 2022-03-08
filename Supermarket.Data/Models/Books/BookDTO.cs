using Supermarket.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Books
{
    public class BookDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int StockNumber { get; set; }

        public string AuthorName { get; set; }

        public int AuthorID { get; set; }

        public BookDTO(Book book)
        {
            Id = book.Id;
            Name = book.Name;
            Price = book.Price;
            Description = book.Description;
            StockNumber = book.StockNumber;
            AuthorName = book.Author.Name;
            AuthorID = book.AuthorId;
        }
    }
}
