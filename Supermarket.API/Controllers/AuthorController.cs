using Microsoft.AspNetCore.Mvc;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models.Authors;
using Supermarket.Data.Models.Helper;

namespace Supermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
           _authorService = authorService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<MessageHelper<List<ListAuthorDTO>>> getAll() {
            var result = await _authorService.GetAll();
            return result;
        }

        [HttpPost]
        [Route("create")]
        public async Task<MessageHelper> Create(CreateAuthorDTO createAuthor)
        {
            var result = await _authorService.Create(createAuthor);
            return result;

        }

        [HttpPost]
        [Route("update")]
        public async Task<MessageHelper<AuthorDTO>> Update(EditAuthorDTO editAuthor)
        {
            return await _authorService.Update(editAuthor);
        }

        [HttpGet("{id}")]
        public async Task<MessageHelper<AuthorDTO>> GetById(int id)
        {
            return await _authorService.GetById(id);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<MessageHelper> Delete(int id)
        {
            return await _authorService.Delete(id);
        }
    }
}
