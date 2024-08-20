using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Areas.Dashboard.Models;
using RecyclableMaterials.Models;
using RecyclableMaterials.ViewModels;

namespace RecyclableMaterials.Data
{
    public class RDBContext :IdentityDbContext
    {


        public RDBContext(DbContextOptions<RDBContext> options) : base(options) { }


        public DbSet<RatingModel> Ratings { get; set; }
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
            builder.Entity<IdentityUser>().ToTable("Users", "sec");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "sec");
        }
        public DbSet<RecyclableMaterials.ViewModels.RegiserViewModel> RegiserViewModel { get; set; } = default!;
        public DbSet<RecyclableMaterials.ViewModels.LoginViewModel> LoginViewModel { get; set; } = default!;
        public DbSet<RecyclableMaterials.ViewModels.RoleFormViewModel> RoleFormViewModel { get; set; } = default!;
        public DbSet<RecyclableMaterials.ViewModels.UserViewModel> UserViewModel { get; set; } = default!;
        //public DbSet<RecyclableMaterials.ViewModels.RegeisterViewModel> RegeisterViewModel { get; set; } = default!;
        //public DbSet<RecyclableMaterials.ViewModels.LoginViewModel> LoginViewModel { get; set; } = default!;
        //public DbSet<RecyclableMaterials.ViewModels.DepartmentViewModel> DepartmentViewModel { get; set; } = default!;
        //public DbSet<RecyclableMaterials.ViewModels.ManageUserViewModel> ManageUserViewModel { get; set; } = default!;
        //public DbSet<RecyclableMaterials.ViewModels.UserViewModel> UserViewModel { get; set; } = default!;
        //public DbSet<RecyclableMaterials.ViewModels.UserRolesViewModel> UserRolesViewModel { get; set; } = default!;

    }
}
