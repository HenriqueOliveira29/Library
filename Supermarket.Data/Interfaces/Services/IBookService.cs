using Supermarket.Data.Entities;
using Supermarket.Data.Models;
using Supermarket.Data.Models.Books;
using Supermarket.Data.Models.Helper;

namespace Supermarket.Data.Interfaces.Services
{
    public interface IBookService
    {
        Task<PaginateList<ListBookDTO>> GetAll(SearchDTO searchDTO);

        Task<MessageHelper> Create(CreateBookDTO createBook);

        Task<MessageHelper<BookDTO>> Update(EditBookDTO editBook);

        Task<MessageHelper<BookDTO>> GetById(int bookId);

        Task<MessageHelper> Delete(int id);
    }
}
