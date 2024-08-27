using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();



var ConnectionString = configuration.GetConnectionString("RConnectionString");


builder.Services.AddDbContext<RDBContext>(Options => Options.UseSqlServer(ConnectionString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()    ////////\\\\\\\\\\
    .AddEntityFrameworkStores<RDBContext>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
