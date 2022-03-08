using Microsoft.AspNetCore.Mvc;
using Supermarket.Data;
using Supermarket.Data.Entities;

namespace Supermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public AuthorController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> getAll() {
            Author escritor = new Author() { 
                Name = "Henrique", 
                AuthorId = 2,
                BirthDate = DateTime.Now,
                DeadDate = DateTime.Now.AddDays(2),
                Books = new List<Book>() {
                    new Book() { Id = 2, Description = "ola", Name="book", Price=30, StockNumber=30, AuthorId = 2},
                     new Book() { Id = 3, Description = "ola2", Name="book", Price=300, StockNumber=30, AuthorId = 2},
                }

            };

            return Ok(escritor);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                await _context.Author.AddAsync(new Author { Name = "Eca de queiros", BirthDate = DateTime.Today, DeadDate = DateTime.Today.AddDays(30)});
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }
    }
}
