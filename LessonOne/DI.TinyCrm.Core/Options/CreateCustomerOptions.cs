using DI.TinyCrm.Core.Dtos;

namespace DI.TinyCrm.Core.Options
{
    public class CreateCustomerOptions
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string VatNumber { get; set; }
        
        public string Address { get; set; }

        public static CreateCustomerOptions MapFromCustomerDto(CustomerDto customer)
        {
            return new CreateCustomerOptions
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                VatNumber = customer.VatNumber,
                Address = customer.Address
            };
        }
    }
}
