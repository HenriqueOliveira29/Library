using Supermarket.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models
{
    public class ListBookDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }  

        public double Price { get; set; }

        public string Description { get; set; }

        public int StockNumber { get; set; }

        public string Author { get; set; }

        public int AuthorID { get; set; }

        public ListBookDTO(Book book )
        {
            Id = book.Id;
            Name = book.Name;
            Price = book.Price;
            Description = book.Description;
            StockNumber = book.StockNumber;
            Author = book.Author.Name;
            AuthorID = book.Author.AuthorId;
        }


    }
}
