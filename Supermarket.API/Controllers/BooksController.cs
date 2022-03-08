using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models;
using Supermarket.Data.Models.Books;
using Supermarket.Data.Models.Helper;

namespace Supermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<MessageHelper<List<ListBookDTO>>> GetAll() {
            var result = await _bookService.GetAll();
            return result;
        }


        [HttpPost]
        [Route("create")]
        public async Task<MessageHelper> Create(CreateBookDTO createBook) {

             return  await _bookService.Create(createBook);
            
        }

        [HttpPost]
        [Route("update")]
        public async Task<MessageHelper<BookDTO>> Update(EditBookDTO editBook) {

            return await _bookService.Update(editBook);
        }

        [HttpGet("{id}")]
        public async Task<MessageHelper<BookDTO>> GetById(int id) {
            return await _bookService.GetById(id);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<MessageHelper> Delete(int id) {
            return await _bookService.Delete(id);
        }
    }
}
