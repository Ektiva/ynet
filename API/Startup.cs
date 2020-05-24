using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using API.Helpers;
using API.Middleware;
using API.Extensions;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        //public void ConfigureDevelopmentServices(IServiceCollection services)
        //{
        //    services.AddDbContext<StoreContext>(x =>
        //        x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

        //    services.AddDbContext<AppIdentityDbContext>(x =>
        //    {
        //        x.UseSqlite(_config.GetConnectionString("IdentityConnection"));
        //    });

        //    ConfigureServices(services);
        //}

        //public void ConfigureProductionServices(IServiceCollection services)
        //{
        //    services.AddDbContext<StoreContext>(x =>
        //        x.UseMySql(_config.GetConnectionString("DefaultConnection")));

        //    services.AddDbContext<AppIdentityDbContext>(x =>
        //    {
        //        x.UseMySql(_config.GetConnectionString("IdentityConnection"));
        //    });

        //    ConfigureServices(services);
        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();

            services.AddDbContext<StoreContext>(x =>
            {
                x.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });

            // For later
            //services.AddSingleton<IConnectionMultiplexer>(c => {
            //    var configuration = ConfigurationOptions.Parse(_config
            //        .GetConnectionString("Redis"), true);
            //    return ConnectionMultiplexer.Connect(configuration);
            //});

            services.AddApplicationServices();

            //services.AddIdentityServices(_config);
            
            services.AddSwaggerDocumentation();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            // To rreplace env.IsDevelopment
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), "Content")
            //    ),
            //    RequestPath = "/content"
            //});

            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseSwaggerDocumention();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapFallbackToController("Index", "Fallback");
            });

        }
    }
}
