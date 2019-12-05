using DigiShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShop.Models
{
    public enum ProductType
    {
        Mobile = 1,
        Laptop = 2,
        Tablet = 3,
    };
    public class ProductItem
    {
        public Guid ProductId { get; set; }
        public List<FeatureItem> Features { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public ProductType ProductType { get; set; }
        public int CountOfAvailable { get; set; }
        public bool IsAvailable => CountOfAvailable > 0;
    }
}
