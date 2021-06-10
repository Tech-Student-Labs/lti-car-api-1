using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.Exception.ExceptionHandlingMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CarDealerAPIService.services;
using Microsoft.AspNetCore.Identity;
using CarDealerAPIService.App.models;

namespace CarDealerWebAPI
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

            services.AddControllers();
            services.AddDbContext<CarDealerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleSubmissionsService, VehicleSubmissionsService>();
            // services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarDealerWebAPI", Version = "v1" });
            });
            services.AddIdentity<User, IdentityRole>(opt=>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).
                AddEntityFrameworkStores<CarDealerContext>()
                .AddRoles<IdentityRole>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarDealerWebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}