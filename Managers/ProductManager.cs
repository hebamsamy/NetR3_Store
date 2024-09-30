using LinqKit;
using Managers;
using Models;
using ViewModels;

namespace Repository
{
    public class ProductManager: MainManager<Product>
    {
        public ProductManager(ProjectContext ProjectContext) : base(ProjectContext) { }

        public List<ProductVeiwModel> Get()
        {
            return GetAll().Select(i=> i.ToVeiwModel()).ToList();
        }

        public Pagination<List<ProductVeiwModel>> Search(
            string? Name = null,
            string? CategoryName = null,
            int CategoryID = 0,
            double Price = 0,
            string OrderBy = "ID",
            bool IsAscending = false,
            int PageSize = 6,
            int PageIndex = 1
            )
        {
            var filter = PredicateBuilder.New<Product>();
            var oldFilter = filter;
            if (!string.IsNullOrEmpty( Name))
            {
                filter = filter.Or(i => i.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(CategoryName))
            {
                filter = filter.Or(i => i.Category.Name.ToLower().Contains(CategoryName.ToLower()));
            }
            if(CategoryID != 0)
            {
                filter = filter.Or(i=>i.CategoryID == CategoryID);
            }
            if (Price != 0)
            {
                filter = filter.And(i => i.Price <= Price);
            }
            if (oldFilter == filter)
            {
                filter = null;
            }
            var count = (filter!=null)? GetAll().Where(filter).Count(): base.GetAll().Count();
            var result = Filter(filter, OrderBy, IsAscending, PageSize, PageIndex);
            return new Pagination<List<ProductVeiwModel>>()
            {
                PageNumber = PageIndex,
                PageSize = PageSize,
                TotalCount = count,
                Data = result.Select(i => i.ToVeiwModel()).ToList()
            };
        }
        public ProductVeiwModel GetOneByID(int id)
        {
            return Get().Where(i => i.ID == id).FirstOrDefault();
        }
        public AddProductViewModel GetEditableByID(int id)
        {
            return GetAll().Where(i => i.ID == id).FirstOrDefault().ToAddViewModel();
        }
        public void Add(AddProductViewModel addProduct)
        {
            var temp = addProduct.ToModel();
            Add(temp);
        }

        public void Edit (AddProductViewModel newPrd)
        {
            var oldprod = GetAll().Where(i => i.ID == newPrd.ID).FirstOrDefault();
            oldprod.Name = newPrd.Name;        
            oldprod.Price = newPrd.Price;        
            oldprod.Quantity = newPrd.Quantity;        
            oldprod.CategoryID = newPrd.CategoryID;
            oldprod.Description = newPrd.Description;
            if (newPrd.KeepImages == false)
            {
                oldprod.ProductAttachments.Clear();
            }
            oldprod.ProductAttachments = new List<ProductAttachment>();
            foreach (var item in newPrd.ImagesURL)
            {
                oldprod.ProductAttachments.Add(new ProductAttachment()
                {
                    Image = item
                });
            }

            Update(oldprod);
        }

        public void Delete(int Id)
        {
            var oldprod = GetAll().Where(i => i.ID == Id).FirstOrDefault();
            Delete(oldprod);
        }

        

    }
}
