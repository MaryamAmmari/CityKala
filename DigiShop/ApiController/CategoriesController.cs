using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigiShop.Repository;
using DigiShop.Models.DataModels;
using DigiShop.Models.ViewModels;

namespace DigiShop.ApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private IProductRepository productRepository;
        public CategoriesController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var list = await productRepository.GetCategoryNameAsync();
            return Json(list);
        }
    }
}