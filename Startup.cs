using AdvApi.Helpers;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AdvApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("\nStartup:ConfigureServices: Configure Services\n");

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Get the name of the database context
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();

            // Determine the type of Database to be connected
            string dbType = appSettings.DbTypeDef.ToUpper();
            Console.WriteLine("Startup:ConfigureServices: DB Type = [" + dbType + "]\n");
            string dbSetting = appSettings.DbDefinition.ToString();
            Console.WriteLine("Startup:ConfigureServices: Connecting to the database\n");

            // Console.WriteLine("ApiContext: DB Setting = [" + dbSetting + "]\n");
            string connectionString = _configuration.GetConnectionString(dbSetting);

            if (dbType == "LITE")
            {
                // connect to sql lite database
                // services.AddDbContext<ApiContext>();
                services.AddDbContext<ApiContext>(options =>
                    options.UseSqlite(connectionString,
                    options => options.MigrationsAssembly("AdvApi")));
            }
            else
            {
                // connect to sql server database
                // services.AddDbContext<ApiContext>();
                services.AddDbContext<ApiContext>(opts =>
                    opts.UseSqlServer(connectionString,
                    options => options.MigrationsAssembly("AdvApi")));
            }

            // Add Controllers
            Console.WriteLine("Startup:ConfigureServices: Adding Controllers\n");
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
