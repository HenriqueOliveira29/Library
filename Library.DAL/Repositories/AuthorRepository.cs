using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public Task<Author> Create(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> Update()
        {
            throw new NotImplementedException();
        }
    }
}
