using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Kursach.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kursach
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //База данных находится в папке пользователя Data Source AttachDbFilename
            string strConn = "Server = (localdb)\\mssqllocaldb;" +
            "Database = DBLibr11; Trusted_Connection = true";

            // Путь к файлу базы данных задано в явном виде
            //string strConn = @"Server=(localdb)\mssqllocaldb; " +
            //                     @"AttachDbFilename=C:\Users\salih\Kurs5.mdf; " +
            //"Trusted_Connection = true;";

            services.AddDbContext<LibraryContext>(
                                   options => options.UseSqlServer(strConn));
            services.AddMvc();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireLogin", policy => policy.RequireAuthenticatedUser());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=HomePage}/{id?}");
            });
        }
    }
}
