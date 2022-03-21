using Microsoft.EntityFrameworkCore;
using Supermarket.Data.Entities;
using Supermarket.Data.Models.AuditTable;

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
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var insertedEntries = this.ChangeTracker.Entries()
								   .Where(x => x.State == EntityState.Added)
								   .Select(x => x.Entity);
			foreach (var insertedEntry in insertedEntries)
			{
				var auditableEntity = insertedEntry as Auditable;
				//If the inserted object is an Auditable. 
				if (auditableEntity != null)
				{
					auditableEntity.DateCreated = DateTimeOffset.UtcNow;
				}
			}
			var modifiedEntries = this.ChangeTracker.Entries()
					   .Where(x => x.State == EntityState.Modified)
					   .Select(x => x.Entity);
			foreach (var modifiedEntry in modifiedEntries)
			{
				//If the inserted object is an Auditable. 
				var auditableEntity = modifiedEntry as Auditable;
				if (auditableEntity != null)
				{
					auditableEntity.DateUpdated = DateTimeOffset.UtcNow;
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}
		public virtual DbSet<Book> Book { get; set; }

        public virtual DbSet<Author> Author { get; set; }
       
    }
}
