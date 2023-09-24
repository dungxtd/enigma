using EnigmaApi.Models;
using EnigmaShared.Configuration;
using EnigmaShared.Filter;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaApi
{
    public class StartUp
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var hostConfig = services.InitConfig<AppConfig>(Configuration);

            services.AddMvc().InitNewtonJson(Configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            services.AddControllers(option =>
            {
                option.Filters.Add<CustomExceptionFilter>();
            });


            ConfigurationExtension.InjectSwagger(services, Configuration);  

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            //app.UseSetAuthContextHandle();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseApm();

            app.UseSwagger(Configuration);


            //app.UserHealCheck
        }
    }
}