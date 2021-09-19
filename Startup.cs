using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyApi.Service.Interfaces;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Repositories;
using FriendlyApi.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace FriendlyApi.Service
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMongoRepository<User>, UserRepository>();

            var conventionPack = new  ConventionPack {new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
            
            var mongoDbConnectionString = _configuration["ConnectionStrings:Mongo"];
            services.AddScoped<IMongoClient, MongoClient>(_ => new MongoClient(MongoClientSettings.FromConnectionString(mongoDbConnectionString)));
            
            services.AddScoped<UserService>();
            services.AddScoped<AuthService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FriendlyApi.Service", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FriendlyApi.Service v1");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}