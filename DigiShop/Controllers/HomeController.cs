using System;
using System.Linq;
using System.Threading.Tasks;
using DigiShop.Models;
using DigiShop.Repository;
using Microsoft.AspNetCore.Mvc;


namespace DigiShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        private AppSettings settings;
        public HomeController(IProductRepository productRepository , AppSettings settings)
        {
            this.productRepository = productRepository;
            this.settings = settings;
        }


        public async Task<IActionResult> Index()
        {
            var x = User;
            var model = new HomeModel()
            {
                Laptops = await productRepository.GetRandomByTypeAsync(ProductType.Laptop, 4),
                Mobiles = await productRepository.GetRandomByTypeAsync(ProductType.Mobile, 4),
            };
            return View(model);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
