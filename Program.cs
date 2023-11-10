using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CRUDTrabajadores.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ContextoBD>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContextoBD") ?? throw new InvalidOperationException("Connection string 'ContextoBD' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trabajador}/{action=Index}/{id?}");

app.Run();
