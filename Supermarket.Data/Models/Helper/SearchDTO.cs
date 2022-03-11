using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Helper
{
    public class SearchDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<Parameter>? Parameters { get; set; }
    }
}
