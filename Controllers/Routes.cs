using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dnc2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        //[HttpGet("{id}")]
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


        //db ef test
        [HttpGet("addToDb/{userName?}/{commentText?}")]
        public string addToDb(string userName= "u",string commentText = "c"){
           using( var ctx = new TestDbContext() ){
               //ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE Users");
               //ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE Comments");
            
               var user = new User(){ Name = userName };
              
               var comment = new Comment(){ Text = commentText };
               var comment2 = new Comment(){ Text = $"{commentText}  2" };


                
                //ctx.Comments.Add(comment);
                user.Comment = new List<Comment>{ comment, comment2 };
                ctx.Users.Add(user);
                 
                ctx.SaveChanges();
           }
           return $"addToDb: {userName}, {commentText}";
        }

        [HttpGet("readDb")]
        public ContentResult readDb(){
            using( var ctx = new TestDbContext() ){
                //var data = ctx.Comments.LastOrDefaultAsync().Result;
                //return $"Comment id =  {data.UserId}, Comment = {data.Text}";
                
                var data = (
                    from c in ctx.Comments
                    join u in ctx.Users
                    on c.UserId equals u.Id  
                    //where u.Id > 5
                    select new{
                        id = u.Id,
                        name = u.Name,
                        comment = c.Text,
                        commentId = c.Id
                    }        
                ).Take(5).OrderByDescending( x => x.id ).ToList();

                string returnData = "";
                foreach( var d in data ){
                    returnData += $"uid: {d.id}, name: {d.name}, comment: {d.comment}, commentId: {d.commentId} <br />";
                }

                return Content(returnData,"text/html");

                /* var test2 = ctx.Comments
                            .Join();   */                       
              
            }
            
           
        }        

    }

    public class Value{
        public int Id{ get; set;}
        
        [MinLength(3)]
        public int Text{ get; set;}
    }
}
