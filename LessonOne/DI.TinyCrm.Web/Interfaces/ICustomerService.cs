using DI.TinyCrm.Web.Entities;
using DI.TinyCrm.Web.Models;
using DI.TinyCrm.Web.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Web.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<List<Customer>>> GetCustomersAsync();

        Task<Result<Customer>> CreateCustomerAsync(CreateCustomerOptions options);

        Task<Result<Customer>> GetCustomerByIdAsync(int id);

        Task<Result<int>> DeleteCustomerByIdAsync(int id);
    }
}
