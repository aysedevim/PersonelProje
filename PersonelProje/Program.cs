using PersonelProje.Models;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<SqlConnection>(); //SqlConnection u bir kere newler. tekrar newleme gerektirmez.
builder.Services.AddScoped<PersonelModel>();  //PersonelModel ý bir kere newler. tekrar newleme gerektirmez. newlemeden kullanýlacak demek.
builder.Services.AddScoped<EgitimModel>();

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
