using System;

namespace DI.Basics.Stage3
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger _logger;

        public CustomerRepository(ILogger logger)
        {
            _logger = logger;
        }

        public void Save()
        {
            _logger.Log("This is CustomerRepository logger.");

            Console.WriteLine("Customer purchase saved.");
        }
    }

    public interface ICustomerRepository
    {
        void Save();
    }
}