using Supermarket.Data.Models.Authors;
using Supermarket.Data.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<MessageHelper<List<ListAuthorDTO>>> GetAll();

        Task<MessageHelper> Create(CreateAuthorDTO createAuthor);

        Task<MessageHelper<AuthorDTO>> Update(EditAuthorDTO editAuthor);

        Task<MessageHelper<AuthorDTO>> GetById(int authorid);

        Task<MessageHelper> Delete(int id);
    }
}
