using DI.TinyCrm.Core.Entities;
using DI.TinyCrm.Core.Models;
using DI.TinyCrm.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Core.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<Product>>> GetProductsAsync();

        Task<Result<Product>> CreateProductAsync(CreateProductOptions options);

        Task<Result<Product>> GetProductByIdAsync(int id);

        Task<Result<int>> DeleteProductByIdAsync(int id);
    }
}
