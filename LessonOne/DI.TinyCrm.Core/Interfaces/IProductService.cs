using DI.TinyCrm.Core.Dtos;
using DI.TinyCrm.Core.Models;
using DI.TinyCrm.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Core.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<ProductDto>>> GetProductsAsync();

        Task<Result<ProductDto>> CreateProductAsync(CreateProductOptions options);

        Task<Result<ProductDto>> GetProductByIdAsync(int id);

        Task<Result<int>> DeleteProductByIdAsync(int id);
    }
}
