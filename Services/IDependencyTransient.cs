using System;

namespace dnc2.Services{

    public interface IDependencyTransient{
        int count{get; set;}
    }

    public class DependencyTransient : IDependencyTransient{
        public int count{get; set;} = 0;

    }
}