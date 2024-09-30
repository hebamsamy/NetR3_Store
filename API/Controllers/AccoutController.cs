using Managers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ViewModels;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private AccountManager accountManager;
        private RoleManager roleManager;
        public AccountController(AccountManager _accountManager, RoleManager _roleManager)
        {
            this.accountManager = _accountManager;
            roleManager = _roleManager;
        }

        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accountManager.Register(viewModel);
                if (result.Succeeded)
                {
                    return new JsonResult("You Successfully Created your Account, Login now");
                }
                else
                {
                    var str = new StringBuilder();
                    foreach (var item in result.Errors)
                    {
                        str.Append(item.Description);
                    }
                    return new JsonResult(str.ToString());
                }
            }
            else
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }

                return new ObjectResult(str);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LogIn(UserLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accountManager.Login(viewModel);
                if(string.IsNullOrEmpty(result))
                {
                    return new JsonResult( new APIResult<string>()
                    {
                        Result = "",
                        Message = "Sorry Your Cridentionals is in valid Try Again!!!",
                        StatusCode = 400,
                        Success = false

                    });
                }
                else if (result == "Failed")
                {
                    return new JsonResult(new APIResult<string>()
                    {
                        Result = "",
                        Message = "Sorry Your Account Is under Review , Try Later!!!",
                        StatusCode = 400,
                        Success = false

                    });
                }
                else{
                    
                    return new JsonResult (new APIResult<string>()
                    {
                        Result = result,
                        Message = "Login Successfully",
                        StatusCode = 200,
                        Success = true

                    });

                }
            }
            else
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }

                return new ObjectResult(str);
            }
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult LogOut()
        {
            accountManager.SignOut();
            return Ok();
        }
    }
}
