using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dnc2.Models;
using dnc2.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dnc2.Controllers{

    [Route("v2")]
    public class ValidationTest2Controller : Controller{
        ValidationTest2Repository repo;// = new ValidationTest2Repository(new TestDbContext());

        public ValidationTest2Controller(ValidationTest2Repository repo){
            this.repo = repo;
        }

        [Route("test")]
        public String test(ValidationTest2ViewModel data){
            

            if( ModelState.IsValid ){
                var vTest = new ValidationTest2(){
                    Name = data.Name,
                    TextData = data.TextData
                };

                try{
                    repo.addData(vTest);

                }catch(DbUpdateException e){                 
                    Console.WriteLine("--------------------"+e.Entries.Count);
                    Console.WriteLine("--------------------"+e.Data.Count);
                    return "DbUpdateException";
                
                }catch(ValidationException e2 ){ //second layer of validation, most likely not needed; overrided SaveChanges in TestDbContext
                    if(ModelState.IsValid){
                        Console.WriteLine("-------db ValidationException true ------------");
                    }else{
                        Console.WriteLine("-------db ValidationException false -----------");
                    }
                    return "ValidationException = " + e2 + "; "+ e2.ValidationAttribute.ErrorMessage;
                }

                return("ValidationTest2ViewModel modelstate valid");
            }else{
                return("ValidationTest2ViewModel Not valid");
            }

        }
    }

    public class ValidationTest2ViewModel{

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name min length = 3")]        
        public string Name { get; set; }
        public string TextData { get; set; }       
    }
}