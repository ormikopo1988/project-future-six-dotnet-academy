using DI.TinyCrm.Web.Persistence;
using DI.TinyCrm.Web.Persistence.Entities;
using DI.TinyCrm.Web.Interfaces;
using DI.TinyCrm.Web.Models;
using DI.TinyCrm.Web.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result<Product>> CreateProductAsync(CreateProductOptions options)
        {
            if (options == null)
            {
                return new Result<Product>(ErrorCode.BadRequest, "Null options.");
            }

            if (string.IsNullOrWhiteSpace(options.Code) ||
              string.IsNullOrWhiteSpace(options.Description) ||
              string.IsNullOrWhiteSpace(options.Name) ||
              options.Price <= 0 ||
              options.Quantity <= 0)
            {
                return new Result<Product>(ErrorCode.BadRequest, "Not all required product options provided.");
            }

            var productWithSameCode = await _context.Products.SingleOrDefaultAsync(pro => pro.Code == options.Code);

            if (productWithSameCode != null)
            {
                return new Result<Product>(ErrorCode.Conflict, $"Product with code #{options.Code} already exists.");
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

                return new Result<Product>(ErrorCode.InternalServerError, "Could not save product.");
            }

            return new Result<Product>
            {
                Data = newProduct
            };
        }

        public async Task<Result<Product>> GetProductByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Result<Product>(ErrorCode.BadRequest, "Id cannot be less than or equal to zero.");
            }

            var product = await _context
                .Products
                .SingleOrDefaultAsync(pro => pro.Id == id);

            if (product == null)
            {
                return new Result<Product>(ErrorCode.NotFound, $"Product with id #{id} not found.");
            }

            return new Result<Product>
            {
                Data = product
            };
        }

        public async Task<Result<int>> DeleteProductByIdAsync(int id)
        {
            var productToDelete = await GetProductByIdAsync(id);

            if (productToDelete.Error != null || productToDelete.Data == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Product with id #{id} not found.");
            }

            _context.Products.Remove(productToDelete.Data);

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

        public async Task<Result<List<Product>>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

            return new Result<List<Product>>
            {
                Data = products.Count > 0 ? products : new List<Product>()
            };
        }
    }
}
