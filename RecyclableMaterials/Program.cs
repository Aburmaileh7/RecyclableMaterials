using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Hubs;
using RecyclableMaterials.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();



var ConnectionString = configuration.GetConnectionString("RConnectionString");


builder.Services.AddSignalR(); // إضافة خدمة SignalR


builder.Services.AddDbContext<RDBContext>(Options => Options.UseSqlServer(ConnectionString));


builder.Services.AddIdentity<AppUserModel, IdentityRole>()   
    .AddEntityFrameworkStores<RDBContext>();



// إذا كنت تستخدم CORS عبر نطاقات مختلفة
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetIsOriginAllowed((host) => true); // يمكنك تقييد السماح للأصول هنا إذا لزم الأمر
    });
});



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


app.UseCors(); // استخدم CORS هنا

app.UseAuthentication(); // تأكد من استخدام المصادقة قبل الترخيص
app.UseAuthorization();


app.MapHub<NotificationHub>("/notificationHub"); // تكوين مسار Hub لـ SignalR



app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
