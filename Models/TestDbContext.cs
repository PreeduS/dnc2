
using Microsoft.EntityFrameworkCore;

namespace dnc2.Models{

    public class TestDbContext : DbContext{
        //public TestDbContext(DbContextOptions<TestDbContext> options) : base(options){ }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Data Source=./sqlite/Test.db");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }


    }

}