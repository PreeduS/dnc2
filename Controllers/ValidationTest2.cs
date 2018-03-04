using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dnc2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dnc2.Controllers{

    [Route("v2")]
    public class ValidationTest2Controller : Controller{

        [Route("test")]
        public String test(ValidationTest2ViewModel data){
            

            if( ModelState.IsValid ){
               //return("ValidationTest2ViewModel valid");
                var vTest = new ValidationTest2(){
                    Name = data.Name,
                    TextData = data.TextData
                };
                using( var ctx = new TestDbContext() ){
                    ctx.ValidationTest2.Add(vTest);


                    try{
                        ctx.SaveChanges();

                    }catch(DbUpdateException e){
                   
                            //return("ValidationTest2ViewModel try not valid, e: "+e);
                  
                        Console.WriteLine("--------------------"+e.Entries.Count);
                        Console.WriteLine("--------------------"+e.Data.Count);

                      
                        
                         Console.WriteLine();
                 
                     //return(e.InnerException.Message);
                    }catch(ValidationException e2 ){

                         if(ModelState.IsValid ){
                             Console.WriteLine("-------ms edit true-----------222--");
                         }else{
                             Console.WriteLine("-------ms edit FALSE-----------222--");
                         }
                       Console.WriteLine("------" + e2.ValidationAttribute.ErrorMessage);


                        return "e2 = " + e2;
                    }



                    
                }
                return("ValidationTest2ViewModel modelstate valid");
            }else{
                return("ValidationTest2ViewModel Not valid");
            }

            //return Json(null);
            //return "null";
        }
    }

    public class ValidationTest2ViewModel{

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name min length = 3")]        
        public string Name { get; set; }
        public string TextData { get; set; }       
    }
}