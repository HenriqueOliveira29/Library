using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Interfaces.Services
{
    public interface IImagesService
    {
        public string UploadImage(int id, IFormFile files);
        public List<string> GetImages();
    }
}
