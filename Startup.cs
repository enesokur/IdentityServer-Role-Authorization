using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using IdentityServerProject;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration
        {
            get;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer().AddDeveloperSigningCredential().AddInMemoryApiScopes(Config.GetApiScopes()).AddInMemoryClients(Config.GetClients()).AddTestUsers(Config.GetUsers()).AddProfileService<ProfileService>();

            services.AddRazorPages();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserSecure", policy => policy.RequireClaim("roleType", "CanReaddata"));
                options.AddPolicy("AdminSecure", policy => policy.RequireClaim("roleType", "CanUpdatedata"));
            });
            services.AddControllers();
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options => {
                options.Authority = "https://localhost:5001";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
            });
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}