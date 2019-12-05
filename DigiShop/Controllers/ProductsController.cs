using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigiShop.Models;
using DigiShop.Repository;
using DigiShop.Models.ViewModels;
using DigiShop.Contexts;

namespace DigiShop.Controllers
{
    public class ProductsController : Controller
    {

        private DataContext _context;

        private IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository, DataContext context)
        {
            this.productRepository = productRepository;
            this._context = context;
        }

        public async Task<IActionResult> List(string category, string search = null, int page = 1)
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
                list = list.Where(i =>
                        i.ProductDescription.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();


            /*--------------------------------Paging---------------------------------*/

            const int itemsPerPage = 12;
            if (page < 1) page = 1;
            var result = _context.products
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                //.Select(i => _mapper.Map<ProductItem>(i))
                .ToList();
            var maxPages = (int)Math.Ceiling((double)_context.products.Count() / itemsPerPage);


            return View(new ProductsListViewModel()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                PagesCount = maxPages,

                //Products = result.Select();
            });

        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var detail = await productRepository.GetByIdAsync(id);
            return View(detail);
        }



    }
}
