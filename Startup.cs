using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dnc2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace dnc2
{
    public class Startup
    {
        public IConfiguration configuration {get; set; }
        public Startup(IConfiguration config){
            configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddDbContext<TestDbContext>(option => option.UseSqlite("Filename=./sqlite/Test.db") );
            //services.AddDbContext<TestDbContext>(option => option.UseSqlite("Data Source=./sqlite/Test.db") );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
                app.UseStaticFiles();

                app.UseMvc();
            
            app.Run(async (context) =>{
                await context.Response.WriteAsync("Fallback testVar = " + configuration["testVar"]);
            });
        }
    }
}
