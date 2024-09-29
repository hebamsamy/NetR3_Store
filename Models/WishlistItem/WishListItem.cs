namespace Models
{
    public class WishListItem
    {
        public int ID { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public string UserID { get; set; }
        public virtual User User { get; set; }
    }
}
