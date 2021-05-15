using System;

namespace DI.Basics.Stage2
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Save()
        {
            Console.WriteLine("Customer purchase saved.");
        }
    }

    public interface ICustomerRepository
    {
        void Save();
    }
}