using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ProjectContext : IdentityDbContext<User>
    {
        public ProjectContext(DbContextOptions options) : base(options)
        { }
        public  DbSet<Category> Categories { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<ProductAttachment> ProductAttachments { get; set; }
        public  DbSet<CartItem> CartItems { get; set; }
        public  DbSet<WishListItem> WishListItems { get; set; }
        public  DbSet<Vendor> Vendors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<ProductAttachment>(new ProductAttachmentConfiguration());
            modelBuilder.ApplyConfiguration<CartItem>(new CartItemConfigration());
            modelBuilder.ApplyConfiguration<WishListItem>(new WishListItemConfigration());
            modelBuilder.ApplyConfiguration<Vendor>(new VendorConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
