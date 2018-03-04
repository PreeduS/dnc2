using Microsoft.AspNetCore.Mvc;


namespace dnc2.Controllers{


    public class FallBackController : Controller{
        
 
        public string beFallback(){
            return "on fallback BE";
        }
        public string feFallback(string url = ""){
            return "on fallback FE "+url;
        }
    }
}