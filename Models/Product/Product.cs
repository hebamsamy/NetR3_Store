namespace Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public string VendorID { get; set; }
        public virtual Vendor Vendor { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductAttachment> ProductAttachments { get; set; }
        public virtual ICollection<WishListItem> WishLists { get; set; }
        public virtual ICollection<CartItem> CartLists { get; set; }
    }
}
