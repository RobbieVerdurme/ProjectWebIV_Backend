using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectWebIV_Backend.Data;
using ProjectWebIV_Backend.Data.Repositories;
using ProjectWebIV_Backend.Models;

namespace ProjectWebIV_Backend
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<PostContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PostContext")));

            services.AddScoped<PostDataInitalizer>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddOpenApiDocument(c => {
                c.DocumentName = "apidocs";
                c.Title = "Post API";
                c.Version = "v1";
                c.Description = "The Post API documentation description.";
            });
            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PostDataInitalizer postDataInitalizer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors("AllowAllOrigins");
            app.UseSwaggerUi3();
            app.UseSwagger();
            postDataInitalizer.InitializeData().Wait();
        }
    }
}
