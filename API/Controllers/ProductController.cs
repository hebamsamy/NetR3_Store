﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModel;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        ProductManager productManager;
        CategoryManeger categoryManeger;
        public ProductController(
            ProductManager _productManager,
            CategoryManeger _categoryManeger)
        {
            productManager = _productManager;
            categoryManeger = _categoryManeger;
        }
        [Authorize(Roles = "Vendor")]
        [HttpGet]
        [Route("VendorList")]
        public IActionResult VendorList()
        {
            return Ok();
        }

       
        [HttpGet]
        [Route("search")]
        public IActionResult Search(
            string? Name = null,
            string? CategoryName = null,
            int CategoryID = 0,
            double Price = 0,
            string OrderBy = "Price",
            bool IsAscending = false,
            int PageSize = 20,
            int PageIndex = 1)
        {
            var list = productManager.Search(Name, CategoryName, CategoryID, Price, OrderBy, IsAscending, PageSize, PageIndex);
            return new ObjectResult(list);
        }

        [Authorize (Roles ="Vendor")]
        [HttpPost]
        [Route("add")]
        public IActionResult Add(AddProductViewModel addProduct)
        {
            addProduct.vendorID = "b5c57c91-4603-4a47-bd0c-d5c3b7f3fd4f";
            if (ModelState.IsValid)
            {
                foreach (IFormFile file in addProduct.Images)
                {
                    string fileName = DateTime.Now.ToFileTime().ToString() + file.FileName;
                    string path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        "Product", fileName
                        );
                    FileStream fileStream = new FileStream(path, FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Close();

                    addProduct.ImagesURL.Add(Path.Combine("Images", "Category", fileName));
                }

                productManager.Add(addProduct);
                return Ok();
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

    }
}
