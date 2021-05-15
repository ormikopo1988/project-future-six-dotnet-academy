using System;

namespace DI.Basics.Stage1
{
    public class CustomerProcessor
    {
        public void UpdateCustomerOrder(string customer, string product)
        {
            var customerRepository = new CustomerRepository();
            var productRepository = new ProductRepository();

            customerRepository.Save();
            productRepository.Save();

            Console.WriteLine(string.Format("Customer record for '{0}' updated with purchase of product '{1}'.", customer, product));
        }
    }
}