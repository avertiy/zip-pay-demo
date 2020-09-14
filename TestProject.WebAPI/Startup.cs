using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TanvirArjel.EFCore.GenericRepository;
using TestProject.WebAPI.Contracts;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.Services;


namespace TestProject.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zip Pay API", Version = "v1" });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            ConfigureDatabaseServices(services);
            services.AddGenericRepository<DataContext>();

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOriginPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build());
            });
        }

        protected virtual void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                    sqlOptions => sqlOptions.MigrationsAssembly("TestProject.WebAPI")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zip Pay API V1");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<DataContext>())
                EnsureDatabaseCreated(context);
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowOriginPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void EnsureDatabaseCreated(DataContext context)
        {
            // run Migrations
            context.Database.Migrate();
        }
    }
}
