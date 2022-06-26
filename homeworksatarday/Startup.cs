using homeworksatarday.Models.Entites;
using homeworksatarday.Repositries;
using homeworksatarday.Repositries.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace homeworksatarday
{
    public class Startup
    {
        //Add-Migration initDatabase
        //Update-Database
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = @"Data Source = THARWATEXAM\\SQL2019STD; Database = HomeWork; User ID = sa; Password = Yemen@134;";
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<MasterDbContext>(op => op.UseSqlServer(connectionString));
            services.AddScoped<IUserRepo, UserRepo>();
        }

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
