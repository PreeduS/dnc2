using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dnc2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration( (context, configBuilder) => {
                    configBuilder.AddJsonFile("mySettings.json");
                    /*if( context.HostingEnvironment.IsDevelopment() ){
                       configBuilder.AddJsonFile("mySettings.dev.json", optional: true);
                       //or maybe use context.HostingEnvironment.EnvironmentName
                    }*/ 
                    //configBuilder.AddEnvironmentVariables();
                })
                .Build();
    }
}
