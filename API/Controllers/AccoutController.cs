using Managers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ViewModel;

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
                if (result.Succeeded)
                {
                    //token
                    //TODO
                    return Ok();

                }
                else if (result.IsLockedOut)
                {
                    return new JsonResult("Sorry Your Account Is under Review , Try Later!!!");
                }
                else
                {
                    return new JsonResult("", "Sorry Your Cridentionals is in valid Try Again!!!");
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
