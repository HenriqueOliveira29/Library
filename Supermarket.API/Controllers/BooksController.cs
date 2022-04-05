using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("getAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<PaginateList<ListBookDTO>> GetAll(SearchDTO search) {
            var result = await _bookService.GetAll(search);
            return result;
        }


        [HttpPost]
        [Route("create")]
        
        public async Task<MessageHelper> Create([FromForm] CreateBookDTO createBook) {
            return  await _bookService.Create(createBook);
            
        }

        [HttpPost]
        [Route("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{nameof(Roles.Admin)}")]
        public async Task<MessageHelper<BookDTO>> Update(EditBookDTO editBook) {

            return await _bookService.Update(editBook);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<MessageHelper<BookDTO>> GetById(int id) {
            return await _bookService.GetById(id);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{nameof(Roles.Admin)}")]
        public async Task<MessageHelper> Delete(int id) {
            return await _bookService.Delete(id);
        }
    }
}
