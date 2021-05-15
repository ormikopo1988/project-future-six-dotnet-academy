using System;

namespace DI.Basics.Stage2
{
    public class ProductRepository : IProductRepository
    {
        public void Save()
        {
            Console.WriteLine("Product inventory updated.");
        }
    }

    public interface IProductRepository
    {
        void Save();
    }
}