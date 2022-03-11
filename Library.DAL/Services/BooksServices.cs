using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models;
using Supermarket.Data.Models.Books;
using Supermarket.Data.Models.Helper;

namespace Library.DAL.Services
{
    public class BooksServices : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public BooksServices(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<MessageHelper> Create(CreateBookDTO createBook)
        {
            MessageHelper result = new ();
           try {

                CreateBookDTOValidator validator = new CreateBookDTOValidator();
                var responseValidate = await validator.ValidateAsync(createBook);
                if (responseValidate == null || responseValidate.IsValid == false) {

                    if (responseValidate == null) {
                        result.Message = "Erro ao validar a informacao.";
                        return result;
                    }

                    result.Message = responseValidate.Errors.FirstOrDefault()!.ErrorMessage;
                    return result;
                }

                var book = await _bookRepository.Create(createBook.ToEntity());

                if (book == null) {

                    result.Sucess = false;
                    result.Message = "Livro criado sem Sucesso";
                    return result;
                }
                result.Sucess = true;
                result.Message = "Livro criado com sucesso";
                
            }
            catch (Exception ex){
                result.Sucess = false;
                result.Message = ex.Message;
                
            }

            return result;
            
        }

        public async Task<MessageHelper> Delete(int id)
        {
            MessageHelper result = new();
            try
            {
                

                var haveBook = _bookRepository.GetById(id).Result;
                if (haveBook == null)
                {
                    result.Sucess = false;
                    result.Message = "Este livro nao existe";
                    return result;
                }

                var deletebook = await _bookRepository.Delete(haveBook);
                if (deletebook == false) {
                    result.Sucess = false;
                    result.Message = "Nao conseguimos eliminar o livro";
                    return result;
                }
                result.Sucess = true;
                result.Message = "Livro Eliminado com Sucesso";
            }
            catch (Exception ex) {
                result.Sucess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<PaginateList<ListBookDTO>> GetAll(SearchDTO search)
        {
            PaginateList<ListBookDTO> result = new PaginateList<ListBookDTO>();
            try
            {
                if (search.PageSize > 100)
                {
                    search.PageSize = 100;
                }

                if (search.PageSize <= 0)
                {
                    search.PageSize = 1;
                }

                if (search.CurrentPage <= 0)
                {
                    search.CurrentPage = 1;
                }

                var responseRepository = await _bookRepository.GetAll(search.Parameters, search.CurrentPage, search.PageSize);

                if (responseRepository.Success != true) {
                    result.Success = false;
                    result.Message = "Erro ao buscar os livros";
                    return result;
                }

                result.Items = responseRepository.Items.Select(t => new ListBookDTO(t)).ToList();
                result.Success = true;
                result.TotalRecords = responseRepository.TotalRecords;
                result.CurrentPage = responseRepository.CurrentPage;
                result.PageSize = responseRepository.PageSize;
                
               
                return result;
                
            }
            catch (Exception ex) {
                result.Success= false;
                return result;
            }
        }

        public async Task<MessageHelper<BookDTO>> GetById(int bookId)
        {
            MessageHelper<BookDTO> result = new();
            var book = await _bookRepository.GetById(bookId);

            if (book == null) {
                result.Sucess = false;
                result.Message = "Nao foi possivel encontrar este livro";
                return result;
            }
            result.obj = new BookDTO(book);
            result.Sucess = true;
            return result;

        }

        public async Task<MessageHelper<BookDTO>> Update(EditBookDTO editBook)
        {
            MessageHelper<BookDTO> result = new();
            try
            {
                EditBookDTOValidator validator = new EditBookDTOValidator();
                var responseValidade = validator.Validate(editBook);
                if (responseValidade.IsValid == false)
                {
                    result.Sucess = false;
                    result.Message = responseValidade.Errors.FirstOrDefault()!.ErrorMessage;
                    return result;
                }
                var book = await _bookRepository.GetById(editBook.Id);
                if (book == null) 
                {
                    result.Sucess=false;
                    result.Message = "Nao existe este livro";
                    return result;

                }
                book.Price = editBook.Price;
                book.Description = editBook.Description;
                book.StockNumber = editBook.StockNumber;
                book.Name = editBook.Name;

                book = await _bookRepository.Update(book);

                result.Sucess = true;
                result.obj = new BookDTO(book);
            }
            catch (Exception ex) {
                result.Sucess = false;
                result.Message = ex.Message;
            }
            return result; 
        }

        
    }
}
