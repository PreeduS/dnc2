using System;
using dnc2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dnc2.Controllers{

    [Route("[controller]")]
    public class DependencyTestController : Controller{
        //public IDependencyA depA;
        //public IDependencyTransient depTransient;
    
        public DependencyTestController(IDependencyA depA,IDependencyTransient depTransient){
            //this.depA = depA;
            //this.depTransient = depTransient;
            depA.count++; //1
            depTransient.count++; //1
            Console.WriteLine("depA Singleton count: "+ depA.count);
            Console.WriteLine("depTransient count: "+ depTransient.count);
        }

        [Route("[action]")]

        public void index(){
          
       
        }

    }

}