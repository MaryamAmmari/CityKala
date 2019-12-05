using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShop.Models.DataModels
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public List<Feature> Features { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public ProductType ProductType { get; set; }
        public int CountOfAvailable { get; set; }
    }
}
