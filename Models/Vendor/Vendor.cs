namespace Models
{
   public class Vendor
    {
        public DateTime JoinedDate {  get; set; }
        public string UserID { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
