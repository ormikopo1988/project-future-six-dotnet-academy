using DI.TinyCrm.Core.Dtos;
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

        public async Task<Result<CustomerDto>> CreateCustomerAsync(CreateCustomerOptions options)
        {
            if (options == null)
            {
                return new Result<CustomerDto>(ErrorCode.BadRequest, "Null options.");
            }

            if (string.IsNullOrWhiteSpace(options.FirstName) ||
              string.IsNullOrWhiteSpace(options.LastName) || 
              string.IsNullOrWhiteSpace(options.Address)|| 
              string.IsNullOrWhiteSpace(options.VatNumber))
            {
                return new Result<CustomerDto>(ErrorCode.BadRequest, "Not all required customer options provided.");
            }

            if (options.VatNumber.Length > 9)
            {
                return new Result<CustomerDto>(ErrorCode.BadRequest, "Invalid vat number.");
            }

            var customerWithSameVat = await _context.Customers.SingleOrDefaultAsync(cus => cus.VatNumber == options.VatNumber);

            if (customerWithSameVat != null)
            {
                return new Result<CustomerDto>(ErrorCode.Conflict, $"Customer with #{options.VatNumber} already exists.");
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

                return new Result<CustomerDto>(ErrorCode.InternalServerError, "Could not save customer.");
            }

            return new Result<CustomerDto>
            {
                Data = CustomerDto.MapFromCustomer(newCustomer)
            };
        }

        public async Task<Result<CustomerDto>> GetCustomerByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Result<CustomerDto>(ErrorCode.BadRequest, "Id cannot be less than or equal to zero.");
            }

            var customer = await _context
                .Customers
                .SingleOrDefaultAsync(cus => cus.Id == id);

            if (customer == null)
            {
                return new Result<CustomerDto>(ErrorCode.NotFound, $"Customer with id #{id} not found.");
            }

            return new Result<CustomerDto>
            {
                Data = CustomerDto.MapFromCustomer(customer)
            };
        }

        public async Task<Result<int>> DeleteCustomerByIdAsync(int id)
        {
            var customerToDelete = await _context
                .Customers
                .SingleOrDefaultAsync(cus => cus.Id == id);

            if (customerToDelete == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Customer with id #{id} not found.");
            }

            _context.Customers.Remove(customerToDelete);

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

        public async Task<Result<List<CustomerDto>>> GetCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();

            return new Result<List<CustomerDto>>
            {
                Data = customers.Count > 0 ? CustomerDto.MapFromCustomer(customers): new List<CustomerDto>()
            };
        }
    }
}
