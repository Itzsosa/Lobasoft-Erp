using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Se configura el esquema de autenticación por medio de cookies en la aplicación web

builder.Services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication",
        config =>
        {
            config.Cookie.Name = "UserLoginCookie";
            config.LoginPath = "/Usuarios/Login";
        });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Lobasoft_Erp.Data.Contexto>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("StringConexion")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
