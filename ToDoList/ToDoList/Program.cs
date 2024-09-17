using Microsoft.EntityFrameworkCore;
using ToDoList.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var connectionStrings = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionStrings);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();