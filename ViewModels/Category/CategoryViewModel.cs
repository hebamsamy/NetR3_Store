using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CategoryViewModel
    {
        public int? Id { get; set; } = 0;

        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string? ImagePath { get; set; } = string.Empty;
    }
}
