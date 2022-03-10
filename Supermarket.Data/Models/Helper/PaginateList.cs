using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Helper
{
    public  class PaginateList<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public List<T> Items { get; set; }

        public int TotalRecords { get; set; } = 0;

        public int Count { get => Items.Count(); }

        public int PageSize { get; set; } = 0;

        public int TotalPages {
            get {
                if (TotalRecords <= 0) return 0;
                return (int)Math.Ceiling((decimal)TotalRecords /PageSize);
            }
        }

        public int CurrentPage { get; set; } = 0;

        public bool HasNextPage { get => TotalPages > CurrentPage; }

        public bool HasPreviousPage {get=> CurrentPage> 1 && TotalPages >= CurrentPage; }

        public PaginateList()
        {
            this.Items = new List<T>();
        }
    }
}
