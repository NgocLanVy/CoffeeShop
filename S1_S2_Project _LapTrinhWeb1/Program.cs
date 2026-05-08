using Microsoft.EntityFrameworkCore;
using S1_S2_Project__LapTrinhWeb1.Data;
using S1_S2_Project__LapTrinhWeb1.Models.Interfaces;
using S1_S2_Project__LapTrinhWeb1.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<CoffeeshopDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopDbContextConnection")));
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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
