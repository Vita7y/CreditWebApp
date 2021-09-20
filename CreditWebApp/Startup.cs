using Credit.BLL;
using Credit.Core.BLL;
using Credit.Core.DAL;
using CreditWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Credit.MockDAL;
using CreditWebApp.Authentication;
using ZNetCS.AspNetCore.Authentication.Basic;

namespace CreditWebApp
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

            services.AddTransient<IClientDataRepository, MockClientDataRepository>();
            services.AddTransient<IClientBL, ClientBL>();

            services.AddTransient<ICreditDataRepository, MockCreditDataRepository>();
            services.AddTransient<ICreditBL, CreditBL>();

            services.AddAutoMapper(cfg => cfg.AddProfile<MapProfile>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CreditWebApp", Version = "v1"});
            });

            services.AddScoped<AuthenticationEvents>();
            services
                .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication(
                    options =>
                    {
                        options.Realm = "Credit WebApi";
                        options.EventsType = typeof(AuthenticationEvents);
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CreditWebApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}