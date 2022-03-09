using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
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
        ApplicationDBContext _context;
        public AuthorRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Author> Create(Author author)
        {
            await _context.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> Delete(Author author)
        {
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var query = await _context.Author.Include(t=> t.Books).ToListAsync();
            return query;
        }

        public async Task<Author> GetById(int id)
        {
            var query = await _context.Author.Where(t => t.AuthorId == id).FirstOrDefaultAsync();
            return query;
        }

        public async Task<Author> Update(Author author)
        {
            _context.Entry<Author>(author).CurrentValues.SetValues(author);
            await _context.SaveChangesAsync();
            return author;

        }
    }
}
