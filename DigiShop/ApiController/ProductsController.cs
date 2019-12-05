using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiShop.Models;
using DigiShop.Models.ViewModels;
using DigiShop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DigiShop.ApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductRepository productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(string category, string search = null)
        {
            List<ProductItem> list;
            if (string.IsNullOrEmpty(category))
                list = await productRepository.GetAllAsync();
            else
            {
                switch (category.ToLower())
                {
                    case "laptop":
                        list = await productRepository.GetByTypeAsync(ProductType.Laptop);
                        break;
                    case "mobile":
                        list = await productRepository.GetByTypeAsync(ProductType.Mobile);
                        break;
                    default:
                        list = new List<ProductItem>();
                        break;
                }
            }
            if (!string.IsNullOrEmpty(search))
                list = list.Where(i => i.ProductDescription.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
            return Json(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var detail = await productRepository.GetByIdAsync(id);
            return Json(detail);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(ProductItem item)
        {
            var result = await productRepository.InsertAsync(item);
            return Json(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put(ProductItem item)
        {
            var result = await productRepository.UpdateAsync(item);
            return Json(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productRepository.DeleteAsync(id);
            return Json(result);
        }
    }
}