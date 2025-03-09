using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieForum2.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieForum2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieForum2Context") ?? throw new InvalidOperationException("Connection string 'MovieForum2Context' not found.")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<MovieForum2Context>();

builder.Services.AddScoped<SignInManager<ApplicationUser>>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
app.MapStaticAssets();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages().WithStaticAssets();

app.Run();
