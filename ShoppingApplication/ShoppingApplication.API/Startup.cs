using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoppingApplication.Data.Models;
using ShoppingApplication.Repository.DistrictRepository;
using ShoppingApplication.Repository.Interfaces;
using ShoppingApplication.Repository.Interfaces.District;
using ShoppingApplication.Repository.Interfaces.Shop;
using ShoppingApplication.Repository.Interfaces.Vendors;
using ShoppingApplication.Repository.ShopRepository;
using ShoppingApplication.Repository.VendorDistrictRepository;
using ShoppingApplication.Repository.VendorRepository;

namespace ShoppingApplication.API
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
            // Add cors
            services.AddCors();

            //db settings
            services.AddSingleton(Configuration);
            var connectionString = Configuration.GetConnectionString("EvaluatorDatabase");
            services.AddDbContext<ShopingContext>(option => option.UseSqlServer(connectionString));

            //inject repo
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IVendorRepository, VendorRepository>();
            services.AddTransient<IShopRepository, ShopRepository>();
            services.AddTransient<IVendorDistrictRepository, VendorDistrictRepository>();

            
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseMvc();
        }
    }
}
