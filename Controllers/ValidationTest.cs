using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dnc2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dnc2.Controllers{

    [Route("v")]
    public class ValidationTest : Controller{

        [Route("test/{user?}")]
        public string Test(VUser user){
          
            if(ModelState.IsValid){
                Console.WriteLine("user IsValid");
            
            }else{
                foreach (var v in ModelState.Values)
                {
                    Console.WriteLine("v = "+ v.RawValue);
                    if(v.Errors.Count != 0){    
                        var e = v.Errors[0];
                        Console.WriteLine("em = "+ e.ErrorMessage);
             
                    }
                     Console.WriteLine();
                }
            }

            return "VUser";
        }
    }


    public class VUser{
       
        [Required(ErrorMessage = "Id required")]
        public int? Id { get; set; }

        [MinLength(3,ErrorMessage = " Name min 3")]
        public string Name { get; set; }
        public string Date { get; set; }
       
    }
}