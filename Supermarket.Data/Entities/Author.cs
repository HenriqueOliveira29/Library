using Supermarket.Data.Models.AuditTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Entities
{
    public class Author : Auditable
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeadDate { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public  ICollection<Book> GetBooks() {
            return Books;
        }


    }
}
