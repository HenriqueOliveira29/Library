using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data.Entities;
using Supermarket.Data.Models.AuditTable;

namespace Supermarket.Data
{
    public class ApplicationDBContext: IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


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

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public virtual DbSet<ApplicationUserRole> ApplicationUsersRoles { get; set; }

    }
}
