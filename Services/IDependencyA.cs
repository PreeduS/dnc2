using System;

namespace dnc2.Services{

    public interface IDependencyA{
        string test{get; set;}
        int count{get; set;}
    }

    public class DependencyA : IDependencyA{
        public string test{get; set;}
        public int count{get; set;}
        public DependencyA(){
            this.test = "test var";
            this.count = 0;
        }
    }
}