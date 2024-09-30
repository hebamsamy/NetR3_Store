using Managers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ViewModels;

namespace API.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private RoleManager manager;
        public RoleController(RoleManager _manager)
        {
            this.manager = _manager;
        }

        [HttpGet]
        [Route("welcome")]
        public IActionResult Index()
        {
            //return new ObjectResult("Hello");
            return new JsonResult("Hello");
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List() { 
            var list = manager.GetAll()
                    .Where(r => r.Name != "Admin")
                    .Select(r => new { Value = r.Name, Text = r.Name }).ToList();
            return new ObjectResult(list);
        }
        [HttpPost]
        //[Route("")]
        public async Task<IActionResult> Add([FromBody]RoleViewModel model) {
            if (ModelState.IsValid) {
                var res = await manager.Add(model.Name);
                if (res.Succeeded)
                {
                    return new JsonResult("Role Added");
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var item in res.Errors)
                    {
                        stringBuilder.Append(item.Description);
                        stringBuilder.Append(", ");
                    }
                    return new JsonResult(stringBuilder.ToString());
                }
            }
            else
            {
                return new JsonResult("Add Role Failed!!!");
            }
        }
    }
}
