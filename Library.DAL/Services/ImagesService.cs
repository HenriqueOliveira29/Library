using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Supermarket.Data.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Services
{
    public class ImagesService : IImagesService
    {
        public IHostingEnvironment hosting;


        public ImagesService()
        {

        }

        public List<string> GetImages()
        {
            throw new NotImplementedException();
        }

        public string UploadImage(int id, IFormFile file)
        {
            try
            {
                if (file != null )
                {
                        FileInfo fi = new FileInfo(file.FileName);
                        var newfilename = "Image_" + id + fi.Extension;
                        var path = Path.Combine("", Assembly.GetEntryAssembly().Location +"\\Images\\" + newfilename);
                        using(var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        return "Inserido com successo";

                    
                }
                return "Sem Imagens para inserir";
            }catch (Exception ex)
            {
                return "Nao foi inserido com successo";
            }
        }
    }
}
