using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Models;
using Library.Repository;
using Library.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library
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
            var connection = Configuration.GetConnectionString("LibraryDatabase");
            services.AddDbContext<LibraryDBContext>(options => options.UseSqlServer(connection));

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";

                });
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookService, BookService>();

            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IReservationService, ReservationService>();

            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ILoginService, LoginService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();
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


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Home}");
                endpoints
                .MapControllerRoute(
                    name: "book",
                    pattern: "{controller=Book}/{action=BookList}/{id?}");
                endpoints
                .MapControllerRoute(
                    name: "reservation",
                    pattern: "{controller=Reservation}/{action}/{id?}");
            });




        }
    }
}
