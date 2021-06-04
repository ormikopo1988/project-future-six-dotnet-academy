using DI.TinyCrm.Core.Entities;
using System.Collections.Generic;

namespace DI.TinyCrm.Core.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string VatNumber { get; set; }

        public string Address { get; set; }

        public static CustomerDto MapFromCustomer(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                VatNumber = customer.VatNumber,
                Address = customer.Address
            };
        }

        public static List<CustomerDto> MapFromCustomer(List<Customer> customers)
        {
            var result = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                result.Add(new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    VatNumber = customer.VatNumber,
                    Address = customer.Address
                });
            }

            return result;
        }
    }
}
