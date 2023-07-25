using Cines_Flagg.Models;
using Microsoft.EntityFrameworkCore;

namespace Cines_Flagg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //----
            //Definimos el coneccion del archivo de configuracion. AHora si tiene para conectarse con la BD.
            builder.Services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("UserContext"));
            });
            //----

            //Sesion de usuario
            builder.Services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute( //Ruta por defecto el index del HOME. Si queremos podemos poner por defecto otro (ej login)
                name: "default",
                pattern: "{controller=Cartelera}/{action=Index}/{id?}");

            app.Run();
        }
    }
}