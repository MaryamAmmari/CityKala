using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShop.Models.DataModels
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
