using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Catalog.Repositories;
using MongoDB.Driver;
using Catalog.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using System.Data.SqlClient;

namespace dotnet
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
             BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));


             //For mongo 
            services.AddSingleton<IMongoClient>(ServiceProvider =>
            {
                var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString);

            });
            //for SQL
             services.AddSingleton<SqlConnection>(ServiceProvider =>
            {
               string connectionString =  @"server=localhost:3306;database=catalog;uid=ryan;pwd=rjKalk1@;";

                return new SqlConnection(connectionString);
            });

            //toggle between which memory source you wanna use 
            //services.AddSingleton<IItemsRepository, InMemItemsRepository>();
            //services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();
            services.AddSingleton<IItemsRepository, SQLDbItemRespository>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet v1"));
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
