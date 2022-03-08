using Microsoft.EntityFrameworkCore;
using Supermarket.Data.Entities;


namespace Supermarket.Data
{
    public class ApplicationDBContext: DbContext 
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Author>(t =>
            {
                t.HasKey(t => t.AuthorId);

            });

            builder.Entity<Book>(t =>
            {
                t.HasKey(t => t.Id);
                t.HasOne(t => t.Author)
                .WithMany(t => t.Books)
                .HasForeignKey(t => t.AuthorId);
            });
        }
        public virtual DbSet<Book> Book { get; set; }

        public virtual DbSet<Author> Author { get; set; }
       
    }
}
