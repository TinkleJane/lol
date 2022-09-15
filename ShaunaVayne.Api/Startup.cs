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
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using ShaunaVayne.Api.StartupExtensions;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using ShaunaVayne.Common.Filters;

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
#if DEBUG   
            services.AddSwaggerGen(x=>
            {
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {   new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] {}}
                });
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Demacia API V1", Version = "v1" });
                x.CustomSchemaIds(y => y.FullName);
                x.DocInclusionPredicate((version, apiDescription) => true);
                x.TagActionsBy(y => new List<string> { y.GroupName });
                x.SupportNonNullableReferenceTypes();
            });
#endif
            var allowSites = Configuration.GetSection("AllowedSites").GetChildren().Select(x => x.Value).ToArray();
            services.AddCors(
                options => options.AddPolicy("AllowCors",
                    builder =>
                    {
                        builder
                            .WithOrigins(allowSites)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    }));
            services.AddJwt();
            services.AddLogging(x =>
            {
                x.AddSerilog();
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
            });


            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly, Assembly.Load("ShaunaVayne.CommandHandler"));
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

            app.UseCors("AllowCors");

#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demacia API V1");
                c.RoutePrefix = string.Empty;
            });
#endif

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
