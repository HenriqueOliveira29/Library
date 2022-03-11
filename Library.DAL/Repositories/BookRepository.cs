
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Models.Helper;

namespace Library.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private ApplicationDBContext _context;
        public BookRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Book> Create(Book book)
        {
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Delete(Book book)
        {
            
             _context.Book.Remove(book);
            await _context.SaveChangesAsync();
          
            return true;
        }

        public async Task<PaginateList<Book>> GetAll(int currentPage = 1, int pageSize = 5)
        {
            PaginateList<Book> response = new PaginateList<Book>();
            var query =  _context.Book.Include(t => t.Author).AsQueryable();

            response.TotalRecords = query.Count();

            var numberOfItemsToSkip = pageSize * (currentPage - 1);
            query = query.Skip(numberOfItemsToSkip);
            query = query.Take(pageSize);

            var list = await query.ToListAsync();

           
            response.Items = list;
            response.CurrentPage = currentPage;
            response.PageSize = pageSize;
            response.Success = true;
            response.Message = null;


            return response;
        }

        public async Task<Book> GetByAuthor(int id)
        {
            var result = await _context.Book.Where(t => t.AuthorId == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Book> GetById(int id)
        {
            var result = await _context.Book.Where(t => t.Id == id).Include(t=>t.Author).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Book> Update(Book book)
        {
             _context.Entry<Book>(book).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
