using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShop.Models.ViewModels
{
    public class CategoryItem
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductItem> ProductItem { get; set; }
    }
}
