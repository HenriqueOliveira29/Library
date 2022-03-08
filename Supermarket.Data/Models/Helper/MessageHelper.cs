using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Helper
{
    public class MessageHelper
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
    }

    public class MessageHelper<T> 
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public T obj { get; set; }
    }
}

