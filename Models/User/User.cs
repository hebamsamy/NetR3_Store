using Microsoft.AspNetCore.Identity;

namespace Models
{
   public class User : IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
        public string NationalID {  get; set; }
        public string Picture { get; set; } = "default.png";
        public virtual Vendor? Vendor { get; set; }
        public virtual ICollection<WishListItem> WishList { get; set; }
        public virtual ICollection<CartItem> CartList { get; set; }

    }
}
