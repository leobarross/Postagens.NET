using Microsoft.EntityFrameworkCore;
using Postagens.NET.Data;
using Postagens.NET.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PostagensDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("StringConexao")));

builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<PublicacaoServices>();
builder.Services.AddScoped<UploadService>();


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
