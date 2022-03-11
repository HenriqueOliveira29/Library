using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Models.Helper;
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

        public async Task<PaginateList<Author>> GetAll(int currentPage = 1, int pageSize = 5)
        {
            PaginateList<Author> response = new PaginateList<Author>();
            var query = _context.Author.Include(t=>t.Books).AsQueryable();

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

        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Author.Include(t=>t.Books).ToListAsync();
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
