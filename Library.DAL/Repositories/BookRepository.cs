
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

        public async Task<IEnumerable<Book>> GetAll()
        {
            var query = await _context.Book.Include(t => t.Author).ToListAsync();
            return query;
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
