using Microsoft.EntityFrameworkCore;
using S1_S2_Project__LapTrinhWeb1.Data;
using S1_S2_Project__LapTrinhWeb1.Models.Interfaces;
using S1_S2_Project__LapTrinhWeb1.Models.Services;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container
builder.Services.AddControllersWithViews();

//add code
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(sp =>ShoppingCartRepository.GetCart(sp));

builder.Services.AddDbContext<CoffeeshopDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopDbContextConnection")));

//session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();