using Managers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repository
{
    public class WishListManager :MainManager<WishListItem>
    {
        ProductManager productManager;
        public WishListManager(ProjectContext ProjectContext, ProductManager _productManager) : base(ProjectContext)
        {
            productManager = _productManager;
        }
        public List<WishListItemViewModel> Get(string UserId)
        {
            return GetAll()
                .Where(i => i.UserID == UserId)
                .Select(i => i.ToViewModel()).ToList();
        }
        public void Add(int ProductID, string UserID)
        {
            var prd = productManager.GetOneByID(ProductID);
            var newitem = new WishListItem()
            {
                ProductID = ProductID,
                UserID = UserID,
            };
            base.Add(newitem);
        }

        public void Delete(int Id)
        {
            var oldprod = GetAll().Where(i => i.ID == Id).FirstOrDefault();
            Delete(oldprod);
        }
    }
}
