using DI.TinyCrm.Core.Entities;
using System.Collections.Generic;

namespace DI.TinyCrm.Core.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public static ProductDto MapFromProduct(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }

        public static List<ProductDto> MapFromProduct(List<Product> products)
        {
            var result = new List<ProductDto>();

            foreach (var product in products)
            {
                result.Add(new ProductDto
                {
                    Id = product.Id,
                    Code = product.Code,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity
                });
            }

            return result;
        }
    }
}
