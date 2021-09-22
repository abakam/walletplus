using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Common;
using WalletPlus.Api.Models.Users;
using WalletPlus.Api.Models.Wallets;
using WalletPlus.Api.Repositories.EFCore;
using WalletPlus.Api.Services.Helpers;

namespace WalletPlus.Api
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
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<WalletPlusDbContext>(opt => opt
                .UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<ITokenHelper, TokenHelper>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Wallet Plus API",
                    Description = "Backend Service for Wallet Plus",
                    TermsOfService = new Uri("https://walletplus.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Wallet Plus API",
                        Email = "contact@walletplus.com",
                        Url = new Uri("https://walletplus.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Wallet Plus API",
                        Url = new Uri("https://walletplus.com/license"),
                    }

                });
               
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(s => s.SerializeAsV2 = true);
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint($"/swagger/v1/swagger.json", "Wallet Plus API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
