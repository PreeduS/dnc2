using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dnc2.Models;
using Microsoft.AspNetCore.Authorization;
//using dnc2.ViewModels;
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
        public async Task<IActionResult> register(){

            if(true){//modelstate later

                var user = new ApplicationUser{
                    UserName = "userD",
                    Email = "emailD@null.null"
                };
                var result = await userManager.CreateAsync(user,"Pass123");
                if(result.Succeeded){
                    await signInManager.SignInAsync(user,false);
                    return Ok("Register Succeeded");
                }else{
                    foreach(var err in result.Errors){
                        ModelState.AddModelError("error_"+err.Code, err.Description);
                    }
                }
            }


            //foreach(KeyValuePair<string, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry> pair in ModelState){
            foreach(var kvp in ModelState){
                Console.WriteLine("-----------------k: "+ kvp.Key);
                foreach(var err in kvp.Value.Errors){
                    Console.WriteLine("-----------------err: "+ err.ErrorMessage);
                }
            }
            var response = (
                from kvp in ModelState
                from err in kvp.Value.Errors
                select new{key = kvp.Key, err= err.ErrorMessage}
            ).ToList();
           
            return Json(response);
            //return Content(ModelState.ErrorCount + ", on register: ");
        }

        [Route("[action]")]
        public async Task<IActionResult> Login(){

            if(User.Identity.IsAuthenticated){
                 return Content("Already logged in as: "+ User.Identity.Name);
            }
            var vm = new{ //temp
                UserName = "userD",
                Password = "Pass123",
                RememberMe = false
                
            };
            var result = await signInManager.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, false);
            if(result.Succeeded){
                return Ok("Login Succeeded");
            }

            ModelState.AddModelError("login_failed", "login failed");
            return Content("login failed");
        }
        
        [Authorize]
        [Route("[action]")]
        public string privateRoute(){
            return "private route";
        }

        [Route("[action]")]
        public string privateRoute2(){
            //HttpContext
            return "private route: " + User.Identity.IsAuthenticated + ", "+ User.Identity.Name;
        }
        
    }
}