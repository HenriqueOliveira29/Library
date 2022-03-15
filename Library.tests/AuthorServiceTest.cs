using Library.DAL.Repositories;
using Library.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Models.Authors;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.tests
{
    public class AuthorServiceTest
    {
        public static ApplicationDBContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            var context = new ApplicationDBContext(options);
            context.Database.EnsureDeleted();
            return context;
        }

        public static AuthorService getService(ApplicationDBContext context)
        {
            BookRepository bookRepository = new BookRepository(context);
            AuthorRepository authorRepository = new AuthorRepository(context);

            AuthorService booksServices = new AuthorService(bookRepository, authorRepository);
            return booksServices;
        }

        public async static Task createAuthors(ApplicationDBContext context)
        {
            Author author = new Author()
            {
                AuthorId = 1,
                Name = "Luis de Camoes",
                BirthDate = DateTime.Now,
                DeadDate = DateTime.Now.AddYears(20),
            };

            await context.AddRangeAsync(author, new Author()
            {
                AuthorId = 2,
                Name = "Eca de queiros",
                BirthDate = DateTime.Now,
                DeadDate = DateTime.Now.AddYears(12),
            });

            await context.SaveChangesAsync();
        }

        [Fact]
        public async void GetAuthor_WhenhaveBook_returnTrue()
        {
            var context = GetContext();
            AuthorService authorServices = getService(context);
            await createAuthors(context);
            var book = await authorServices.GetById(2);
            Assert.True(book.Sucess);
        }

        [Fact]
        public async void GetAuthor_WhenDonthaveBook_returnFalse()
        {
            var context = GetContext();
            AuthorService authorServices = getService(context);
            await createAuthors(context);
            var author = await authorServices.GetById(4);
            Assert.False(author.Sucess);
        }

        [Fact]
        public async void CreateAuthor_WhenHasAuthor_returnFalse() {
            var context = GetContext();
            AuthorService authorServices = getService(context);
            await createAuthors(context);
            CreateAuthorDTO author = new CreateAuthorDTO()
            {
                Name = "Lusiadas",
                BirthDate = DateTime.Now,
                DeadDate = DateTime.Now.AddYears(20),
            };
            var response = await authorServices.Create(author);
            Assert.False(response.Sucess);
        }
    }
}
