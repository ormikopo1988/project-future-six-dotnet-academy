using DI.TinyCrm.Core.Dtos;
using DI.TinyCrm.Core.Models;
using DI.TinyCrm.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<List<CustomerDto>>> GetCustomersAsync();

        Task<Result<CustomerDto>> CreateCustomerAsync(CreateCustomerOptions options);

        Task<Result<CustomerDto>> GetCustomerByIdAsync(int id);

        Task<Result<int>> DeleteCustomerByIdAsync(int id);
    }
}
