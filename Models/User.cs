using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dnc2.Models{

    public class User : DbContext {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Comment> Comment { get; set; }  

        public UserDetails UserDetails{ get; set; }  //one to one convention

    }

}