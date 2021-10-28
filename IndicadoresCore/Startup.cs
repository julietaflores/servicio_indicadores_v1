using IndicadoresCore.GraphQL;
using IndicadoresCore.Models;
using IndicadoresCore.Services;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndicadoresCore.Models.BC;

namespace IndicadoresCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors();


            services.AddDbContext<DatabaseContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            services.AddScoped<Query>();
            services.AddScoped<Mutuation>();
            services.AddScoped<IService, Service>();
            services.AddGraphQL(c => SchemaBuilder.New().AddServices(c).AddType<GraphQLTypes>()
                                                                        .AddQueryType<Query>()
                                                                          .AddMutationType<Mutuation>()
                                                                         .Create());
        }


        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins(Configuration["ApplicationSettings:IP_cors"].ToString().Split(","))
             .AllowAnyMethod()
             .AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/api",
                    Path = "/metod"
                });
            }
            app.UseGraphQL("/api");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
