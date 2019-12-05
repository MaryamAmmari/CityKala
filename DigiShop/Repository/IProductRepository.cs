using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigiShop.Models;
using DigiShop.Models.DataModels;
using DigiShop.Models.ViewModels;

namespace DigiShop.Repository
{
    public interface IProductRepository
    {
        Task<bool> DeleteAsync(Guid id);
        Task<List<ProductItem>> GetAllAsync();
        Task<ProductItem> GetByIdAsync(Guid id);
        Task<List<ProductItem>> GetByTypeAsync(ProductType productType);
        Task<List<ProductItem>> GetRandomByTypeAsync(ProductType productType, int count);
        Task<Product> InsertAsync(ProductItem item);
        Task<Product> UpdateAsync(ProductItem item);
        Task<List<CategoryItem>> GetCategoryNameAsync();
    }
}