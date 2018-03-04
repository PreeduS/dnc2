using Microsoft.AspNetCore.Mvc;


namespace dnc2.Controllers{

    [Route("TestRoute")]
    [Route("TestRoute2")]

    public class TestRouteController : Controller{
        
        [Route("test")]
        public string test(){
            return "on test";
        }
    }
}