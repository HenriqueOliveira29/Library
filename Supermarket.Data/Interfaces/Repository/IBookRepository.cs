using Supermarket.Data.Entities;
using Supermarket.Data.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Interfaces.Repository
{
    public interface IBookRepository
    {
        public Task<Book> Create(Book book);

        public Task<Book> Update(Book book);

        public Task<PaginateList<Book>> GetAll(int currentPage = 1, int pageSize = 5);

        public Task<Book> GetById(int id);

        public Task<Book> GetByAuthor(int id);

        public Task<bool> Delete(Book book);
    }
}
