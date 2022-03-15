using Library.DAL.Repositories;
using Library.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Models.Books;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.tests
{
    public class BookServiceTest
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

        public static BooksServices getService (ApplicationDBContext context) {
            BookRepository bookRepository = new BookRepository(context);
            AuthorRepository authorRepository = new AuthorRepository(context);

            BooksServices booksServices = new BooksServices(bookRepository, authorRepository);
           return booksServices;
        }

        public async static Task createBooks(ApplicationDBContext context) {
            Author author = new Author()
            {
                AuthorId = 1,
                Name = "Luis de Camoes",
                BirthDate = DateTime.Now,
                DeadDate = DateTime.Now.AddYears(20),
            };

            await context.AddRangeAsync(author,
               new Book()
               {
                   Id = 1,
                   Name = "Lusiadas",
                   Description = "ola",
                   Price = 30,
                   StockNumber = 10,
                   AuthorId = 1,
                   Author = author,
               }, new Book()
               {
                   Id=2,
                   Name = "nseimais",
                   Description = "ola",
                   Price = 20,
                   StockNumber = 10,
                   AuthorId = 1,
                   Author = author,
               });
            await context.SaveChangesAsync();
        }

        [Fact]
        public async void GetBook_WhenhaveBook_returnTrue()
        {
            var context = GetContext();
            BooksServices booksServices = getService(context);
            await createBooks(context);
            var book = await booksServices.GetById(2);
            Assert.True(book.Sucess);
        }

        [Fact]
        public async void GetBook_WhenDontHaveBook_returnFalse() {
            var context = GetContext();
            BooksServices booksServices = getService(context);
            await createBooks(context);
            var book = await booksServices.GetById(4);
            Assert.False(book.Sucess);
        }

        [Fact]
        public async Task CreateBook_WhenCreate_returnTrue() {
            var context = GetContext();
            BooksServices booksServices = getService(context);
            await createBooks(context);
            CreateBookDTO book = new CreateBookDTO() {
                Name = "Maias",
                Description = "OLA",
                StockNumber = 10,
                Price = 5,
                AuthorId = 1,
                
            };
            var response = await booksServices.Create(book);
            Assert.True(response.Sucess);
        }

        [Fact]
        public async Task CreateBook_WhenDontHaveName_returnFalse()
        {
            var context = GetContext();
            BooksServices booksServices = getService(context);
            await createBooks(context);
            CreateBookDTO book = new CreateBookDTO()
            {
                Name = "",
                Description = "OLA",
                StockNumber = 10,
                Price = 5,
                AuthorId = 1,

            };
            var response = await booksServices.Create(book);
            Assert.False(response.Sucess);
        }
        [Fact]
        public async Task CreateBook_WhenAlreadyHaveThisBook_returnFalse() {
            var context = GetContext();
            BooksServices booksServices = getService(context);
            await createBooks(context);
            CreateBookDTO book = new CreateBookDTO()
            {
                Name = "Lusiadas",
                Description = "OLA",
                StockNumber = 10,
                Price = 5,
                AuthorId = 1,

            };
            var response = await booksServices.Create(book);
            Assert.False(response.Sucess);
        }

    }
}