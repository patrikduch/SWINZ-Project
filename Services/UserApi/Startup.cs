﻿//-----------------------------------------------------------------------
// <copyright file="Startup.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UserApi.Helpers;
using UserApi.Interfaces.Helpers;
using UserApi.Interfaces.Repositories;
using UserApi.Interfaces.UnitOfWork;
using UserApi.Mocking;
using UserApi.QueryObjects;
using UserApi.Repositories;
using UserApi.UnitOfWork;

namespace UserApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Contexts;

    /// <summary>
    /// Init configuration setup of UserApi service
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
            #region Only for migrations
            // Entity framework context setup
            var conn = Configuration.GetConnectionString("Default");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(conn));
            #endregion

            #region CORS
            // Cors services
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));
            #endregion


            #region Authentication

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsHelper>(appSettingsSection);


        


            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettingsHelper>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            #endregion


            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerUnitOfWork, CustomerUnitOfWork>();
            services.AddScoped<IUserContextService, UserContextService>();
            // Helper services
            services.AddScoped<IUserHelperService, UserHelperService>();

            // Query object services
            services.AddScoped<ICustomerQuery, CustomerQuery>();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // CORS
            app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // CORS
                //app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            }

            app.UseMvc();
        }
    }
}
