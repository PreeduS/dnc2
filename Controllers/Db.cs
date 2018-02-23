using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dnc2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace dnc2.Controllers{

    [Route("db")]
    public class DbController : Controller{
        
        [HttpGet("getData")]
        public IActionResult getData(){
            using( var ctx = new TestDbContext() ){
                var data = ctx.Users
                    .Select( x => new { id = x.Id, name = x.Name } )
                    .Where( x => x.id > 10)
                    .Take(3)
                    .ToList();

                return Json(data);
            }  
        }
        [HttpGet("getData2")]
        public IActionResult getData2(){
            using( var ctx = new TestDbContext() ){
                var data = ctx.Users
                    .Include( u=> u.Comment)
                    /*.Select( u => new{
                        Name = u.Name,
                        Comment = u.Comment.Select( z => z.Text)
                    } )*/
                    .ToList();
             
                
                var userList = new List<UserMappings>();
                foreach (var d in data)
                {
                    Console.WriteLine("name = " + d.Name);     

                    var comments = new List<string>(); 
                    foreach (var c in d.Comment) {
                        Console.WriteLine("comment = " + c.Text);
                        comments.Add( c.Text  );
                    }

                    userList.Add( new UserMappings{
                        Id = d.Id,
                        Name = d.Name,   
                        Comment = comments                
                    });    

                    Console.WriteLine();
                }

                



                return Json(userList);
                /*,new JsonSerializerSettings
                {
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });*/
            }  
        }


    }

    class UserMappings{
        public int Id{ get; set; }
        public string Name{ get; set; }

        public List<string> Comment;
    }
}

/*
.FirstOrDefault(x=>...)
 */