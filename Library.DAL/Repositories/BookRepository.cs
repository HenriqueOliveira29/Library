
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Models.AuditTable;
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
            if (typeof(Auditable).IsAssignableFrom(typeof(Book)))
            {
                (book as Auditable).DateDeleted = DateTimeOffset.UtcNow;
                _context.Entry<Book>(book).CurrentValues.SetValues(book);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<PaginateList<Book>> GetAll(List<Parameter>? SearchBy,List<Parameter>? OrderBy, int currentPage = 1, int pageSize = 5)
        {
            PaginateList<Book> response = new PaginateList<Book>();
           
            var query =  _context.Book.Include(t => t.Author).Where(a=> a.DateDeleted == null).AsQueryable();

            SearchBy = Parameter.VerParametros(new string[] {
                "name", "price", "stockNumber", "author", "all"}, SearchBy);

            if (SearchBy.Count() > 0) {

                foreach (var parameter in SearchBy) {
                    if (parameter.Value != null) {

                        switch (parameter.Name) {
                              case "name":
                                query = query.Where(t => t.Name.ToUpper().Contains(parameter.Value.Trim().ToUpper()));
                                break;
                              case "price":
                                query = query.Where(t => t.Price.ToString().Contains(parameter.Value));
                                break;
                            case "stockNumber":
                                query = query.Where(t => t.StockNumber.ToString().Contains(parameter.Value));
                                break;
                            case "author":
                                query = query.Where(t => t.Author.Name.ToUpper().Contains(parameter.Value.Trim().ToUpper()));
                                break;
                            case "all":
                                query = query.Where(t => t.Name.ToUpper().Contains(parameter.Value.Trim().ToUpper()) || t.Price.ToString().Contains(parameter.Value) || t.StockNumber.ToString().Contains(parameter.Value) || t.Author.Name.ToUpper().Contains(parameter.Value.Trim().ToUpper()) || t.Id.ToString().Contains(parameter.Value.Trim().ToUpper()));
                                break;


                        }
                    }
                }
            }

            response.TotalRecords = query.Count();

            if (OrderBy != null && OrderBy.Count > 0) {
                switch (OrderBy[0].Name)
                {
                    case "name":
                        if (OrderBy[0].Value!.ToUpper() == "ASC")
                        {
                            query = query.OrderBy(t => t.Name);
                        }
                        else {
                            query = query.OrderByDescending(t => t.Name);
                        }
                        break;
                    case "price":
                        if (OrderBy[0].Value!.ToUpper() == "ASC") 
                        {
                            query = query.OrderBy(t => t.Price);
                        }
                        else
                        {
                            query = query.OrderByDescending(t => t.Price);
                        }
                        break;
                    case "stockNumber":
                        if (OrderBy[0].Value!.ToUpper() == "ASC")
                        {
                            query = query.OrderBy(t => t.StockNumber);
                        }
                        else
                        {
                            query = query.OrderByDescending(t => t.StockNumber);
                        }
                        break;
                    case "author":
                        if (OrderBy[0].Value!.ToUpper() == "ASC")
                        {
                            query = query.OrderBy(t => t.Author.Name);
                        }
                        else
                        {
                            query = query.OrderByDescending(t => t.Author.Name);
                        }
                        break;
                    
                        
                }
            }

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
            var result = await _context.Book.Where(t => t.AuthorId == id).Where(a=> a.DateDeleted == null).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Book> GetById(int id)
        {
            var result = await _context.Book.Where(t => t.Id == id).Include(t=>t.Author).Where(a => a.DateDeleted == null).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Book> GetByName(string name)
        {
            var result = await _context.Book.Where(t => t.Name.ToUpper().Contains(name.Trim().ToUpper())).Where(a => a.DateDeleted == null).FirstOrDefaultAsync();
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
