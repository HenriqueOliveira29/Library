using Supermarket.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Authors
{
    public class AuthorDTO
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeadDate { get; set; }

        public AuthorDTO(Author author)
        {
             AuthorId = author.AuthorId;
             Name = author.Name;
             BirthDate = author.BirthDate;
             DeadDate = author.DeadDate;
        }
        public Author ToEntity() {
            return new Author { 
                AuthorId = AuthorId,
                Name = Name,
                BirthDate = BirthDate,
                DeadDate = DeadDate
            };
        }


    }
}
