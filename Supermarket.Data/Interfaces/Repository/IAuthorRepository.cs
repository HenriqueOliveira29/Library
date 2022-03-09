using Supermarket.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Interfaces.Repository
{
    public interface IAuthorRepository
    {
        Task<Author> Create(Author author);

        Task<Author> GetById(int id);

        Task<IEnumerable<Author>> GetAll();

        Task<Author> Update(Author author);

        Task<bool> Delete(Author author);
    }
}
