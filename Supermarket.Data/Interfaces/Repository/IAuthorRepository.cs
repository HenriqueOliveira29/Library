using Supermarket.Data.Entities;
using Supermarket.Data.Models.Helper;
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

        Task<PaginateList<Author>> GetAll(List<Parameter> SearchBy, List<Parameter> OrderBy, int currentPage = 1, int pageSize = 5);

        Task<List<Author>> GetAuthors();

        Task<Author> Update(Author author);

        Task<bool> Delete(Author author);

        public Task<Author> GetByName(string name);
    }
}
