using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShop.Models.ViewModels
{
    public class ProductsListViewModel
    {

        public List<ProductItem> Products { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
