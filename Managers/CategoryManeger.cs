using Managers;
using Models;

namespace Repository
{
    public class CategoryManeger:MainManager<Category>
    {
        public CategoryManeger(ProjectContext ProjectContext) :base(ProjectContext) { }    
    }
}
