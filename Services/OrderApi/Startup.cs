﻿//-----------------------------------------------------------------------
// <copyright file="Startup.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace OrderApi
{
    using OrderApi.Interfaces.Repositories;
    using OrderApi.Interfaces.UnitOfWork;
    using QueryObjects;
    using Repositories;
    using UnitOfWork;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Contexts;

    /// <summary>
    /// Init configuration setup of OrderApi service
    /// </summary>
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Entity framework context setup
            var conn = Configuration.GetConnectionString("Default");
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(conn));

            services.AddScoped<IProductUnitOfWork, ProductUnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();

            services.AddScoped<IOrderQuery, OrderQuery>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // CORS
                app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            }

            app.UseMvc();
        }
    }
}
