using Models;

namespace ViewModels.Wishlist
{
    public class WishListItemViewModel
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public List<string> ProductImages { get; set; }
    }
    public static class WishEx
    {
        public static WishListItemViewModel ToViewModel(this WishListItem item)
        {
            return new WishListItemViewModel
            {
                ID = item.ID,
                UserId = item.UserID,
                ProductId = item.ProductID,
                ProductName = item.Product.Name,
                ProductPrice = item.Product.Price,
                ProductImages = item.Product.ProductAttachments.Select(x => x.Image).ToList(),

            };
        }
    }
}

