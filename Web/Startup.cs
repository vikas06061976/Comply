 using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.OpenApi.Models;  
using ComplyExchangeCMS.Web.Middleware;
using Microsoft.AspNetCore.Cors.Infrastructure; 
using System.Data.SqlClient;
using System.Data;
using ComplyExchangeCMS.Persistence;
namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
          services.AddInfrastructure();
            services.AddControllers();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComplyExchangeCMS", Version = "v1" }));

            services.AddScoped<IDbConnection>(provider =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                return new SqlConnection(connectionString);
            });

            services.AddTransient<ExceptionHandlingMiddleware>();

            #region CoreSettings           
            var corsBuilder = new CorsPolicyBuilder();
            /*Cors*/
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();

            #region Stage
            corsBuilder.WithOrigins("https://localhost:3000",
            "https://122.176.101.76:8089",
            "http://localhost:3000",
            "http://122.176.101.76:8089");

            #endregion

            #region Live
            //corsBuilder.WithOrigins("https://localhost:3000",
            //"https://122.176.101.76:8089",
            //"http://122.176.101.76:8089", "http://localhost:3000");

            #endregion

            corsBuilder.AllowCredentials();
           
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", corsBuilder.Build());
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
  )
        
        {
            app.UseCors("AllowSpecificOrigins");
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
            // }
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
           // loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

    }
}
