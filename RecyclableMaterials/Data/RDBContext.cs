using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Areas.Dashboard.Models;
using RecyclableMaterials.Models;
using RecyclableMaterials.ViewModels;

namespace RecyclableMaterials.Data
{
    public class RDBContext :IdentityDbContext<AppUserModel>
    {

        public RDBContext(DbContextOptions<RDBContext> options) : base(options) { }

        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<ProductModel> products { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "sec");
            builder.Entity<IdentityRole>().ToTable("Roles", "sec");

            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "sec");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "sec");

            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "sec");
            builder.Entity<AppUserModel>().ToTable("Users", "sec");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "sec");
        }
       
    }
}
