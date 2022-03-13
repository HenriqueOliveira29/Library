
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

        public async Task<PaginateList<Book>> GetAll(List<Parameter>? parameters, int currentPage = 1, int pageSize = 5)
        {
            PaginateList<Book> response = new PaginateList<Book>();
           
            var query =  _context.Book.Include(t => t.Author).AsQueryable();

            parameters = Parameter.VerParametros(new string[] {
                "name", "price", "stockNumber", "author"}, parameters);

            if (parameters.Count() > 0) {

                foreach (var parameter in parameters) {
                    if (parameter.Value != null) {

                        switch (parameter.Name) {
                              case "name":
                                query = query.Where(t => t.Name.ToUpper().Contains(parameter.Value.ToUpper()));
                                break;
                              case "price":
                                query = query.Where(t => t.Price.ToString().Contains(parameter.Value));
                                break;
                            case "stockNumber":
                                query = query.Where(t => t.StockNumber.ToString().Contains(parameter.Value));
                                break;
                            case "author":
                                query = query.Where(t => t.Author.Name.ToUpper().Contains(parameter.Value.ToUpper()));
                                break;
            
                                
                        }
                    }
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
