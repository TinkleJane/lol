using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShaunaVayne.Data;
using ShaunaVayne.Infrastructure.Security;
using ShaunaVayne.Infrastructure.Validation;
using System.Reflection;
using ShaunaVayne.CommandHandler;
using ShaunaVayne.Bus.Command;
using ShaunaVayne.Bus;
using ShaunaVayne.Api.ModelBinder;

namespace ShaunaVayne.Api
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
            var modelBinder = new GeneralCommandModelBinderProvider();
            services.AddDbContext<DemaciaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("demacia"),
                    opt => { opt.MigrationsAssembly(typeof(Startup).Assembly.FullName); }));
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, modelBinder);
            });
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly,
                Assembly.Load("ShaunaVayne.CommandHandler"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(ICommandHandler<>),typeof(GeneralCommandHandler<>));
            services.AddScoped<IBus, InMemoryBus>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
