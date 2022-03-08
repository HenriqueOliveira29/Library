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
        public Task<Author> Create(Author author);

        public Task<Author> GetById(int id);

        public Task<IEnumerable<Author>> GetAll();

        public Task<Author> Update();
    }
}
