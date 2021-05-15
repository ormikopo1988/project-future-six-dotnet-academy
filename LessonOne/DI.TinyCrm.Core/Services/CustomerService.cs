using DI.TinyCrm.Core.Entities;
using DI.TinyCrm.Core.Interfaces;
using DI.TinyCrm.Core.Models;
using DI.TinyCrm.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IApplicationDbContext context, ILogger<CustomerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result<Customer>> CreateCustomerAsync(CreateCustomerOptions options)
        {
            if (options == null)
            {
                return new Result<Customer>(ErrorCode.BadRequest, "Null options.");
            }

            if (string.IsNullOrWhiteSpace(options.FirstName) ||
              string.IsNullOrWhiteSpace(options.LastName) || 
              string.IsNullOrWhiteSpace(options.Address)|| 
              string.IsNullOrWhiteSpace(options.VatNumber))
            {
                return new Result<Customer>(ErrorCode.BadRequest, "Not all required customer options provided.");
            }

            if (options.VatNumber.Length > 9)
            {
                return new Result<Customer>(ErrorCode.BadRequest, "Invalid vat number.");
            }

            var customerWithSameVat = await _context.Customers.SingleOrDefaultAsync(cus => cus.VatNumber == options.VatNumber);

            if (customerWithSameVat != null)
            {
                return new Result<Customer>(ErrorCode.Conflict, $"Customer with #{options.VatNumber} already exists.");
            }

            var newCustomer = new Customer
            {
                VatNumber = options.VatNumber,
                FirstName = options.FirstName,
                LastName = options.LastName,
                Address = options.Address
            };

            await _context.Customers.AddAsync(newCustomer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<Customer>(ErrorCode.InternalServerError, "Could not save customer.");
            }

            return new Result<Customer>
            {
                Data = newCustomer
            };
        }

        public async Task<Result<Customer>> GetCustomerByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Result<Customer>(ErrorCode.BadRequest, "Id cannot be less than or equal to zero.");
            }

            var customer = await _context
                .Customers
                .SingleOrDefaultAsync(cus => cus.Id == id);

            if (customer == null)
            {
                return new Result<Customer>(ErrorCode.NotFound, $"Customer with id #{id} not found.");
            }

            return new Result<Customer>
            {
                Data = customer
            };
        }

        public async Task<Result<int>> DeleteCustomerByIdAsync(int id)
        {
            var customerToDelete = await GetCustomerByIdAsync(id);

            if (customerToDelete.Error != null || customerToDelete.Data == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Customer with id #{id} not found.");
            }

            _context.Customers.Remove(customerToDelete.Data);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<int>(ErrorCode.InternalServerError, "Could not delete customer.");
            }

            return new Result<int>
            {
                Data = id
            };
        }

        public async Task<Result<List<Customer>>> GetCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();

            return new Result<List<Customer>>
            {
                Data = customers.Count > 0 ? customers : new List<Customer>()
            };
        }
    }
}
