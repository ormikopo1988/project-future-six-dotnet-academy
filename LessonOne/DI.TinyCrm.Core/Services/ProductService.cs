using DI.TinyCrm.Core.Dtos;
using DI.TinyCrm.Core.Entities;
using DI.TinyCrm.Core.Interfaces;
using DI.TinyCrm.Core.Models;
using DI.TinyCrm.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result<ProductDto>> CreateProductAsync(CreateProductOptions options)
        {
            if (options == null)
            {
                return new Result<ProductDto>(ErrorCode.BadRequest, "Null options.");
            }

            if (string.IsNullOrWhiteSpace(options.Code) ||
              string.IsNullOrWhiteSpace(options.Description) ||
              string.IsNullOrWhiteSpace(options.Name) ||
              options.Price <= 0 ||
              options.Quantity <= 0)
            {
                return new Result<ProductDto>(ErrorCode.BadRequest, "Not all required product options provided.");
            }

            var productWithSameCode = await _context.Products.SingleOrDefaultAsync(pro => pro.Code == options.Code);

            if (productWithSameCode != null)
            {
                return new Result<ProductDto>(ErrorCode.Conflict, $"Product with code #{options.Code} already exists.");
            }

            var newProduct = new Product
            {
                Name = options.Name,
                Price = options.Price,
                Code = options.Code,
                Description = options.Description,
                Quantity = options.Quantity
            };

            await _context.Products.AddAsync(newProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<ProductDto>(ErrorCode.InternalServerError, "Could not save product.");
            }

            return new Result<ProductDto>
            {
                Data = ProductDto.MapFromProduct(newProduct)
            };
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Result<ProductDto>(ErrorCode.BadRequest, "Id cannot be less than or equal to zero.");
            }

            var product = await _context
                .Products
                .SingleOrDefaultAsync(pro => pro.Id == id);

            if (product == null)
            {
                return new Result<ProductDto>(ErrorCode.NotFound, $"Product with id #{id} not found.");
            }

            return new Result<ProductDto>
            {
                Data = ProductDto.MapFromProduct(product)
            };
        }

        public async Task<Result<int>> DeleteProductByIdAsync(int id)
        {
            var productToDelete = await _context
                .Products
                .SingleOrDefaultAsync(pro => pro.Id == id);

            if (productToDelete == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Product with id #{id} not found.");
            }

            _context.Products.Remove(productToDelete);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<int>(ErrorCode.InternalServerError, "Could not delete product.");
            }

            return new Result<int>
            {
                Data = id
            };
        }

        public async Task<Result<List<ProductDto>>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

            return new Result<List<ProductDto>>
            {
                Data = products.Count > 0 ? ProductDto.MapFromProduct(products) : new List<ProductDto>()
            };
        }
    }
}
