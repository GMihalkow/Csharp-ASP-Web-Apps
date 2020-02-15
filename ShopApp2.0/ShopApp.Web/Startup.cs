using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApp.Dal;
using ShopApp.Dal.Repositories;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category;
using ShopApp.Dal.Services.Category.Contracts;
using ShopApp.Dal.Services.Product;
using ShopApp.Dal.Services.Product.Contracts;
using ShopApp.Dal.Services.User;
using ShopApp.Dal.Services.User.Contracts;
using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Web.Filters.Result;
using ShopApp.Web.Services.Account;
using ShopApp.Web.Services.Account.Contracts;
using ShopApp.Web.Services.Role;
using ShopApp.Web.Services.Role.Contracts;

namespace ShopApp.Web
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
            services.AddDbContext<ShopAppDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseMySql(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddIdentity<ShopUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ShopAppDbContext>();

            services.AddAuthentication()
                .AddFacebook(opts =>
                {
                    opts.AppId = Configuration["FacebookAppId"];
                    opts.AppSecret = Configuration["FacebookAppSecret"];
                });

            services.AddAuthorization();

            services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(typeof(ProvideCategoriesResultFilterAttribute));
                })
                .AddRazorRuntimeCompilation();

            services.AddAntiforgery();

            this.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ShopAppDbContext>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRepository<CategoryViewModel, CategoryBaseInputModel>, CategoryRepository>();
            services.AddScoped<IRepository<ProductViewModel, ProductBaseInputModel>, ProductRepository>();
        }
    }
}