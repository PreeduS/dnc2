using System;
using dnc2.Models;

namespace dnc2.Repos{


    public class ValidationTest2Repository {
        private TestDbContext ctx;// = new TestDbContext();
        public ValidationTest2Repository(TestDbContext ctx){
            this.ctx = ctx;
        }
        public void addData(ValidationTest2 vTest){
            ctx.ValidationTest2.Add(vTest);
            ctx.SaveChanges();

        }
    }
}