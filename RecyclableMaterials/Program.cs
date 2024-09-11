using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Middelwares;
using RecyclableMaterials.Models;
using RecyclableMaterials.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();



// إعداد Hangfire مع SQL Server


var ConnectionString = configuration.GetConnectionString("RConnectionString");





builder.Services.AddDbContext<RDBContext>(Options => Options.UseSqlServer(ConnectionString));

builder.Services.AddHangfire(configuration => configuration.UseSqlServerStorage(ConnectionString));////////

builder.Services.AddHangfireServer();////////////

builder.Services.AddIdentity<AppUserModel, IdentityRole>()   
    .AddEntityFrameworkStores<RDBContext>();


// تسجيل خدمة الإشعارات
builder.Services.AddScoped<INotificationService, NotificationService>();//////////

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


app.UseCors();

app.UseAuthentication();
app.UseAuthorization();


app.UseHangfireDashboard();/////////////////


app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCustomMiddleware();

app.Run();
