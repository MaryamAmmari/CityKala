using DigiShop.Contexts;
using DigiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigiShop.Models.DataModels;
using DigiShop.Models.ViewModels;

namespace DigiShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext dataContext;
        public ProductRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<ProductItem>> GetAllAsync()
        {
            var list = await dataContext.products.Select(i => new ProductItem()
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProductType = i.ProductType,
                ProductPrice = i.ProductPrice,
                ProductImage = i.ProductImage,
                ProductDescription = i.ProductDescription

            }).ToListAsync();
            return list;
        }

        public async Task<List<ProductItem>> GetByTypeAsync(ProductType productType)
        {
            return await dataContext.products.Where(i => i.ProductType == productType)
                .Select(i => new ProductItem()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    ProductType = i.ProductType,
                    ProductPrice = i.ProductPrice,
                    ProductImage = i.ProductImage,
                    ProductDescription = i.ProductDescription
                }).ToListAsync();
        }


        public async Task<List<CategoryItem>> GetCategoryNameAsync()
        {
            return await dataContext.categories.Select(i => new CategoryItem()
            {
                CategoryId = i.CategoryId,
                CategoryName = i.CategoryName
            }).ToListAsync();
        }

        public async Task<List<ProductItem>> GetRandomByTypeAsync(ProductType productType, int count)
        {
            return await dataContext.products
                .Where(i => i.ProductType == productType)
                .OrderBy(i => Guid.NewGuid())
                .Take(count)
                .Select(i => new ProductItem()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    ProductType = i.ProductType,
                    ProductPrice = i.ProductPrice,
                    ProductImage = i.ProductImage,
                    ProductDescription = i.ProductDescription
                })
                .ToListAsync();
        }

        //todo (with outomapper - return view - for detail Page (table))

        public async Task<ProductItem> GetByIdAsync(Guid id)
        {
            var result = await dataContext.products
                .Include(i => i.Features)
                .Where(i => i.ProductId == id)
                .FirstOrDefaultAsync();
            return null;
        }

        public async Task<Product> InsertAsync(ProductItem item)
        {
            var product = new   Product()        
            {
                ProductId = Guid.NewGuid(),
                ProductName = item.ProductName,
                ProductType = item.ProductType,
                ProductPrice = item.ProductPrice,
                ProductImage = item.ProductImage,
                ProductDescription = item.ProductDescription
            };
            dataContext.products.Add(product);

            if ((await dataContext.SaveChangesAsync()) > 0)
                return product;
            else
                return null;
        }


        public async Task<Product> UpdateAsync(ProductItem item)
        {
            var product = await dataContext.products.FirstOrDefaultAsync(i => i.ProductId == item.ProductId);
            if (product == null)
                return null;
            product.ProductName = item.ProductName;
            product.ProductType = item.ProductType;
            product.ProductPrice = item.ProductPrice;
            product.ProductImage = item.ProductImage;
            product.ProductDescription = item.ProductDescription;

            if ((await dataContext.SaveChangesAsync()) > 0)
                return product;
            else
                return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await dataContext.products.FirstOrDefaultAsync(i => i.ProductId == id);
            if (product == null)
                return false;
            dataContext.products.Remove(product);
            return (await dataContext.SaveChangesAsync()) > 0;
        }
    }
}
