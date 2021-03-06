﻿using System;
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
using dnc2.Services;
using dnc2.Configs;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Identity;
using dnc2.Repos;

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
            //services.AddMvc();
            //services.AddMvc(options => options.Conventions.Insert(0,new ModeRouteConvention( route => new List<string>{"aaa","Routes","ccc"}.Contains(route, StringComparer.Ordinal)    ) ) );
            services.AddMvc(options => options.Conventions.Insert(0,new ModeRouteConvention() ) );
            
            //services.AddDbContext<TestDbContext>(option => option.UseSqlite("Data Source=./sqlite/Test.db") );
            services.AddDbContext<TestDbContext>();
            services.AddScoped<ValidationTest2Repository>();

          
            services.AddSingleton<IDependencyA,DependencyA>();  
            services.AddTransient<IDependencyTransient,DependencyTransient>();  //AddScoped

            services.AddIdentity<ApplicationUser,IdentityRole>()
            .AddEntityFrameworkStores<TestDbContext>()                                       //.AddErrorDescriber<IdentityErrorDescriber>()
            .AddDefaultTokenProviders();

            /*services.AddAuthentication()
                .AddJwtBearer(options =>{
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                        //...
                    };
                }
            );
            */
            services.Configure<IdentityOptions>(options =>{
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options => {
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
                options.Cookie.Expiration = TimeSpan.FromDays(14);
                //options.LoginPath = "/path/path";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //app.UseIdentity();
            app.UseAuthentication();

            app.UseDefaultFiles( new DefaultFilesOptions(){
                FileProvider = new PhysicalFileProvider( Path.Combine(Directory.GetCurrentDirectory(), "appPublic") ),
                DefaultFileNames = new List<string>{"index.html"},
                RequestPath = "" 
            });
            app.UseStaticFiles( new StaticFileOptions(){
                FileProvider = new PhysicalFileProvider( Path.Combine(Directory.GetCurrentDirectory(), "appPublic") ),
                RequestPath = ""   //   "/app"                
            } );

            app.UseMvc();
            app.UseMvc( routes =>{
                    /*routes.MapRoute( 
                        name: "default", 
                        template: "{controller}/{action}" 
                    );*/          
                    
                    routes.MapRoute( 
                        name: "fallback-route-be", 
                        template: "api/{*url}",
                        defaults: new { controller = "FallBack", action = "beFallback" }
                    );
                        routes.MapRoute( 
                        name: "fallback-route-fe", 
                        template: "{*url}",   //   {url:regex(^(?!api).*$)}
                        defaults: new { controller = "FallBack", action = "feFallback" }
                    );                                        
                
                }
            );
        
            app.Run(async (context) =>{
                //await context.Response.WriteAsync("Fallback testVar = " + configuration["testVar"] );
                await context.Response.SendFileAsync( Path.Combine(Directory.GetCurrentDirectory(), "appPublic","index.html") );
            });
        }
    }
}
