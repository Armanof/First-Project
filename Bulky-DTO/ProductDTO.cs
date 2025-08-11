using Bulky_DTO.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_DTO
{
    public class ProductDTO
        : BaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public Guid? F_CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
