using Managers;
using Models;
using ViewModels;

namespace Repository
{
    public class CartManager : MainManager<CartItem>
    {
        ProductManager productManager;
        public CartManager(ProjectContext ProjectContext, ProductManager _productManager) : base(ProjectContext)
        {
            productManager = _productManager;
        }
        public List<CartItemViewModel> Get(string UserId)
        {
            return GetAll()
                .Where(i=>i.UserID==UserId)
                .Select(i => i.ToViewModel()).ToList();
        }
        public void Add(int ProductID, string UserID)
        {
            var prd = productManager.GetOneByID(ProductID);  
            var newcart = new CartItem()
            {
                ProductID = ProductID,
                UserID = UserID,
                Quantity = 1,
                SupPrice = prd.Price,
            };
            base.Add(newcart);
        }
        public void Update(int ProductID, string UserID, int newQty)
        {
            var data = GetAll().Where(i=>i.ProductID == ProductID && i.UserID== UserID).FirstOrDefault();
            data.Quantity = newQty;
            var prd = productManager.GetOneByID(ProductID);
            data.SupPrice = prd.Price * newQty;
            base.Update(data);

        }
        public void Delete(int Id)
        {
            var oldprod = GetAll().Where(i => i.ID == Id).FirstOrDefault();
            Delete(oldprod);
        }
    }
}
