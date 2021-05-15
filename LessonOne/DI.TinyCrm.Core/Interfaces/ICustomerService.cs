using DI.TinyCrm.Core.Entities;
using DI.TinyCrm.Core.Models;
using DI.TinyCrm.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<List<Customer>>> GetCustomersAsync();

        Task<Result<Customer>> CreateCustomerAsync(CreateCustomerOptions options);

        Task<Result<Customer>> GetCustomerByIdAsync(int id);

        Task<Result<int>> DeleteCustomerByIdAsync(int id);
    }
}
