using DI.TinyCrm.Core.Dtos;

namespace DI.TinyCrm.Core.Options
{
    public class CreateProductOptions
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public static CreateProductOptions MapFromProductDto(ProductDto product)
        {
            return new CreateProductOptions
            {
                Code = product.Code,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}
