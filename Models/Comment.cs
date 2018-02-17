using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace dnc2.Models{

    public class Comment : DbContext {
        
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }
        
        public User User { get; set; }

    }

}