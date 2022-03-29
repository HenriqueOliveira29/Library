using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Models.Helper
{
    public class EmailFileDTO
    {
        public string Filename { get; set; }
        public string Path { get; set; }
        public string Source { get; set; }

        public string? Content { get; internal set; }
        public string? Type { get; internal set; }

        public EmailFileDTO(string filename, string path = null, string source = null, string? content = null, string? type = null)
        {
            Filename = filename;
            Path = path;
            Source = source;
            Content = content;
            Type = type;
        }
    }
}
