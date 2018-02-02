using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace dnc2.Controllers
{
    [Route("Routes")]
    public class RoutesController : Controller
    {   
        [HttpGet]
        public string test(){
            return "test method";
        }

        //[Route("{id}")]
        //[Route("{id?}")]         //optional
        //[Route("{id=123}")]      //default value
        //[Route("{id:int}")]      //type constraint 
        [HttpGet("{id}")]
         public string test(int id){
            return $"test method, id = {id}";
        }


        //[FromBody]
        //[FromQuery]
        //someFunc( [FromQuery] int id) //route params have priority over query params

        [HttpPost]
        public void Post([FromBody] Value value ){
            if(!ModelState.IsValid){
                throw new InvalidOperationException("Invalid");
            }
        }
        public IActionResult Post2([FromBody] Value value ){
            if(!ModelState.IsValid){
               return BadRequest(ModelState);
            }
            
            return CreatedAtAction("Get",
                new { id = value.Id },
                value
            );

        }        

    }

    public class Value{
        public int Id{ get; set;}
        
        [MinLength(3)]
        public int Text{ get; set;}
    }
}
