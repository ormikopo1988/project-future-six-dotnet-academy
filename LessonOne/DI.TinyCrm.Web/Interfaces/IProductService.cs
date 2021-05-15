using DI.TinyCrm.Web.Data.Entities;
using DI.TinyCrm.Web.Models;
using DI.TinyCrm.Web.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Web.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<Product>>> GetProductsAsync();

        Task<Result<Product>> CreateProductAsync(CreateProductOptions options);

        Task<Result<Product>> GetProductByIdAsync(int id);

        Task<Result<int>> DeleteProductByIdAsync(int id);
    }
}
