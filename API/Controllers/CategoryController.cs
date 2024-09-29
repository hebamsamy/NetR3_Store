using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private CategoryManeger Maneger;
        public CategoryController(CategoryManeger _maneger) {
            Maneger = _maneger;
        }
        [HttpGet]
        [Route("list")]
        public IActionResult list()
        {
            var list = Maneger.GetAll().ToList();
            return new JsonResult (list);
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid) {
                string fileName = DateTime.Now.ToFileTime().ToString() + viewModel.Image.FileName;
                string path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Images",
                    "Category", fileName
                    );
                FileStream fileStream = new FileStream(path, FileMode.Create);
                viewModel.Image.CopyTo(fileStream);
                fileStream.Close();

                viewModel.ImagePath = Path.Combine("Images", "Category", fileName);

                var res =  Maneger.Add(viewModel.ToModel());
                if (res)
                {
                    return Ok ("Added Successfully");
                }
                else
                {
                    
                    return BadRequest("Sorry Failed To Add");
                }

            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                //foreach (var item in ModelState.V)
                //{
                //    stringBuilder.Append(item.Description);
                //    stringBuilder.Append(", ");
                //}
                return new JsonResult(stringBuilder.ToString());
            }
           
        }
    }
}
