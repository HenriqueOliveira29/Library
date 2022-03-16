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

        public async Task<PaginateList<Author>> GetAll(List<Parameter> SearchBy,List<Parameter>? OrderBy, int currentPage = 1, int pageSize = 5)
        {
            PaginateList<Author> response = new PaginateList<Author>();

            var query = _context.Author.Include(t=>t.Books).AsQueryable();

            SearchBy = Parameter.VerParametros(new string[] { "name", "all" }, SearchBy);

            if (SearchBy.Count() > 0) {

                foreach (var parameter in SearchBy) {
                    if (parameter.Value != null) {
                        switch (parameter.Name) {
                            case "name":
                                query = query.Where(t => t.Name.ToUpper().Contains(parameter.Value.Trim().ToUpper()));
                                break;
                            case "all":
                                query = query.Where(t => t.Name.ToUpper().Contains(parameter.Value.Trim().ToUpper()) || 
                                t.BirthDate.ToString().Contains(parameter.Value) || 
                                t.DeadDate.ToString().Contains(parameter.Value) || 
                                t.Books.Count().ToString().ToUpper().Contains(parameter.Value.Trim().ToUpper()) || 
                                t.AuthorId.ToString().Contains(parameter.Value.Trim().ToUpper()));
                                break;
                        }
                    }
                }
            }

            if (OrderBy != null && OrderBy.Count > 0)
            {
                switch (OrderBy[0].Name)
                {
                    case "name":
                        if (OrderBy[0].Value!.ToUpper() == "ASC")
                        {
                            query = query.OrderBy(t => t.Name);
                        }
                        else
                        {
                            query = query.OrderByDescending(t => t.Name);
                        }
                        break;
                    case "numberBook":
                        if (OrderBy[0].Value!.ToUpper() == "ASC")
                        {
                            query = query.OrderBy(t => t.Books.Count());
                        }
                        else
                        {
                            query = query.OrderByDescending(t => t.Books.Count());
                        }
                        break;
                    case "birthDate":
                        if (OrderBy[0].Value!.ToUpper() == "ASC")
                        {
                            query = query.OrderBy(t => t.BirthDate);
                        }
                        else
                        {
                            query = query.OrderByDescending(t => t.BirthDate);
                        }
                        break;
                    case "deadDate":
                        if (OrderBy[0].Value!.ToUpper() == "ASC")
                        {
                            query = query.OrderBy(t => t.DeadDate);
                        }
                        else
                        {
                            query = query.OrderByDescending(t => t.DeadDate);
                        }
                        break;
                }
            }

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

        public async Task<Author> GetByName(string name)
        {
            var result =  await _context.Author.Where(t=>t.Name.ToUpper().Contains(name.Trim().ToUpper())).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Author> Update(Author author)
        {
            _context.Entry<Author>(author).CurrentValues.SetValues(author);
            await _context.SaveChangesAsync();
            return author;

        }
    }
}
