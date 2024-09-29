using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class CatergoryExtainsion
    {
        public static Category ToModel(this  CategoryViewModel viewModel)
        {
            return new Category
            {
                Name = viewModel.Name,
                Image = viewModel.ImagePath
            };
        }
    }
}
