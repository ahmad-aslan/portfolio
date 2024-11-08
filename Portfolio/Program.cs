using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.DataAccess.Repository;
using Portfolio.DataAccess.DbInitializer;
using Microsoft.Extensions.DependencyInjection;
using Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// initializ database
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
// Add Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISenderEmail, EmailSender>();

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
app.UseAuthentication();
app.UseAuthorization();
SeedDatabase();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Public}/{controller=Home}/{action=Index}/{id?}");

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitialiaer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitialiaer.Initilaizer();
    }
}