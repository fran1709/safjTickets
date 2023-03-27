using devTicket.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("DevTicketContextConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DevTicketContext>(options => {
    options.UseMySql(conn, new MySqlServerVersion(new Version(10, 4, 27)));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
