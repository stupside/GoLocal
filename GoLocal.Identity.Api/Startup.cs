using GoLocal.Identity.Application;
using GoLocal.Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GoLocal.Identity.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            
            services.SetupApplication();
            services.SetupInfrastructure(_configuration);
            
            services.AddCors(options =>
                options.AddPolicy(
                    "Clients", p => 
                        p.WithOrigins("https://localhost:5002")
                            .AllowAnyMethod().AllowAnyMethod())
            );
            
            services.AddControllers();
            services.AddRazorPages();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "GoLocal.Identity.Api", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoLocal.Identity.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Clients");
            
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}