using Supermarket.Data.Models.AuditTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Entities
{
    public class Book : Auditable
    {
       public int Id { get; set; }

       public string Name { get; set; }

       public double Price { get; set; }

       public string Description { get; set; }
        
       public int StockNumber { get; set; }

       public int AuthorId { get; set; }

       public Author Author { get; set; }




    }
}
