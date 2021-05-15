using System;

namespace DI.Basics.Stage2
{
    public class CustomerProcessor : ICustomerProcessor
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public CustomerProcessor(ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public void UpdateCustomerOrder(string customer, string product)
        {
            _customerRepository.Save();
            _productRepository.Save();

            Console.WriteLine(string.Format("Customer record for '{0}' updated with purchase of product '{1}'.", customer, product));
        }
    }

    public interface ICustomerProcessor
    {
        void UpdateCustomerOrder(string customer, string product);
    }
}