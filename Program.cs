using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// 🔹 Lấy chuỗi kết nối
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 🔹 Đăng ký DbContext
builder.Services.AddDbContext<WebBanhangDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
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
app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
