﻿using Models;

namespace ViewModels
{
    public static class ProductExtaintions
    {
        public static Product ToModel(this AddProductViewModel addProduct)
        {
            var ProductAttachments = new List<ProductAttachment>();
            foreach (var item in addProduct.ImagesURL)
            {
                ProductAttachments.Add(new ProductAttachment()
                {
                    Image = item
                });
            }
            return new Product
            {
                ID = addProduct.ID??0,
                Name = addProduct.Name,
                Price = addProduct.Price,
                Quantity = addProduct.Quantity,
                Description = addProduct.Description,
                CategoryID = addProduct.CategoryID,
                ProductAttachments = ProductAttachments,
                VendorID = addProduct.vendorID,
            };
        }
        public static AddProductViewModel ToAddViewModel(this Product Product)
        {
            return new AddProductViewModel() 
            {
                ID = Product.ID,
                Name = Product.Name,
                Price = Product.Price,
                Quantity = Product.Quantity,
                Description = Product.Description,
                CategoryID = Product.CategoryID,
                ImagesURL = Product.ProductAttachments.Select(x =>x.Image).ToList(),
                vendorID = Product.VendorID,
            };
        }
        public static Product ToModel(this ProductVeiwModel addProduct)
        {
            var ProductAttachments = new List<ProductAttachment>();
            foreach (var item in addProduct.Images)
            {
                ProductAttachments.Add(new ProductAttachment()
                {
                    Image = item
                });
            }
            return new Product
            {
                ID = addProduct.ID,
                Name = addProduct.Name,
                Price = addProduct.Price,
                Quantity = addProduct.Quantity,
                Description = addProduct.Description,
                CategoryID = addProduct.CategoryId,
                ProductAttachments = ProductAttachments,
                VendorID = addProduct.VendorID,
            };
        }
        public static ProductVeiwModel ToVeiwModel(this Product product)
        {
            return new ProductVeiwModel
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                CategoryId = product.CategoryID,
                CategoryName = product.Category.Name,
                Images = product.ProductAttachments.Select(x => x.Image).ToList(),
                VendorID= product.VendorID,
            };
        }
    }
}
