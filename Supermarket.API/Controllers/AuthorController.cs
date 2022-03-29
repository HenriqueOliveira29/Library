using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("getAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<PaginateList<ListAuthorDTO>> getAll(SearchDTO search) {
            var result = await _authorService.GetAll(search);
            return result;
        }

        [HttpGet]
        [Route("getAuthors")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<MessageHelper<List<ListAuthorDTO>>> getAuthors() {
            return await _authorService.GetAuthors();
        }

        [HttpPost]
        [Route("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{nameof(Roles.Admin)}")]
        public async Task<MessageHelper> Create(CreateAuthorDTO createAuthor)
        {
            var result = await _authorService.Create(createAuthor);
            return result;

        }

        [HttpPost]
        [Route("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{nameof(Roles.Admin)}")]
        public async Task<MessageHelper<AuthorDTO>> Update(EditAuthorDTO editAuthor)
        {
            return await _authorService.Update(editAuthor);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<MessageHelper<AuthorDTO>> GetById(int id)
        {
            return await _authorService.GetById(id);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{nameof(Roles.Admin)}")]
        public async Task<MessageHelper> Delete(int id)
        {
            return await _authorService.Delete(id);
        }
    }
}
