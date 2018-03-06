
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dnc2.Models{

    //public class TestDbContext : DbContext{
    public class TestDbContext : IdentityDbContext<ApplicationUser>{
        //public TestDbContext(DbContextOptions<TestDbContext> options) : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Data Source=./sqlite/Test.db");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ValidationTest2> ValidationTest2 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            foreach (var entity in modelBuilder.Model.GetEntityTypes() ){
                //entity.Relational().TableName = "tbl_" + entity.ClrType.Name;
                Console.WriteLine($"entity = {entity}, ClrType = {entity.ClrType.Name}" );
            
                foreach (var prop in entity.GetProperties().Where(p => p.ClrType == typeof(string)) ){
                    //prop.SetMaxLength(200);
                    Console.WriteLine($"prop = {prop}" );
                }            
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(){
            var errors = from e in ChangeTracker.Entries()
                       where e.State == EntityState.Added
                           || e.State == EntityState.Modified
                       select e.Entity;
        
            foreach(var err in errors){
               // Console.WriteLine("err --"+err.GetType().);
              
                    Validator.ValidateObject(
                        err, 
                        new ValidationContext(err),
                        validateAllProperties: true );
            }
            return base.SaveChanges();

        }

    }

}