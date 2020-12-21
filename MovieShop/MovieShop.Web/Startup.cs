using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repositories;
using MovieShop.Infrastructure.Services;

namespace MovieShop.Web
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
            services.AddControllersWithViews();
            services.AddDbContext<MovieShopDbContext>(optionsAction: options => 
            options.UseSqlServer(Configuration.GetConnectionString(("MovieShopDbConnection"))));
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAsyncRepository<Genre>, EfRepository<Genre>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IAsyncRepository<UserRole>, EfRepository<UserRole>>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICastRepository,CastRepository>();
            services.AddScoped<ICastService, CastService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            

           //sets the default authentication scheme for the app
           //auth middleware:
           services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "MovieShopAuthCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.LoginPath = "/Account/Login";
            });

            //1:30pm, MovieShopAuthCookie created.
            //now it's 4:00 pm, then it will check if the MovieShopAuthCookie is present or not, and if it's expired
            //since it's expired, it will take you to the login page: "/Account/Login"

            // check if the MovieShopAuthCookie is present or not, and if it's expired
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
            }
            app.UseStaticFiles(); 

            //this is a middleware. 
            //It has the pattern matching technique.
            //(it takes Url and check:if the attributes match the 
            //controller and the methods)
            app.UseRouting();
            //this one and the .UserEndpoints are responsible for routing URL to
            //correct controllers and action methods.

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //http://example.com/  => if you don't provide controller and method, it'll go to default.
                //http://example.com/Students/Index  => now Students is gonna override Home, and go to Index method
                //http://example.com/Movies => Will go to Movies controller and Index method
                //http://example.com/Movies/Create => Will go to Movies controller and Create method.
                //http://example.com/Movies/Details/22 => Will go to the movieDetail with id of 22.
                //http://example.com/Movies/List/2019/December => Now it won't work, so we need a new pattern.
            });



            //Middleware: When you make a request in ASP.NET code,
            //the request will go through some middlewares.
        }
    }
}
