using System;

namespace DI.Basics.Stage3
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger _logger;

        public ProductRepository(ILogger logger)
        {
            _logger = logger;
        }

        public void Save()
        {
            Console.WriteLine("Product inventory updated.");

            _logger.Log("HELLO ATHENS!");
        }
    }

    public interface IProductRepository
    {
        void Save();
    }
}