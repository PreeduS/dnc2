using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace dnc2.Models{

    public class UserDetails : DbContext {
 
        
        public string Email { get; set; }
        public string RegisterDate { get; set; }

        //[ForeignKey("User")]
        public int UserId { get; set; }  //one to one convention
        public User User{ get; set; }    //one to one convention

    }

}