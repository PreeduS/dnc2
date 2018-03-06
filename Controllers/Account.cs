using dnc2.Models;
using dnc2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dnc2.Controllers{

    [Route("[controller]")]
    public class AccountController : Controller{

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController( UserManager<ApplicationUser> userManager, 
                                  SignInManager<ApplicationUser> signInManager){
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        
        //public IActionResult register(RegisterViewModel vm){
        [Route("[action]")]
        public IActionResult register(){

            return Content("on register");
        }
    }
}