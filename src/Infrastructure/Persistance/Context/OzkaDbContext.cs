using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Persistance.Context
{
    public class OzkaDbContext : IdentityDbContext<User, Role, int>
    {
        public OzkaDbContext(DbContextOptions<OzkaDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "ozkainsaat",
                    Password = "admin123"
                }
            );

            base.OnModelCreating(builder);
        }

        public DbSet<Cities> Cities { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }




        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var properties = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in properties)
            {

                switch (entity.State)
                {
                    case EntityState.Deleted:
                        entity.Entity.UpdatedDate = DateTime.Now;
                        entity.Entity.IsDeleted = true;
                        break;
                    case EntityState.Modified:
                        entity.Entity.UpdatedDate = DateTime.Now;
                        entity.Entity.IsDeleted = false;
                        break;
                    case EntityState.Added:
                        entity.Entity.CreatedDate = DateTime.Now;
                        entity.Entity.IsDeleted = false;
                        break;
                    default:
                        break;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
