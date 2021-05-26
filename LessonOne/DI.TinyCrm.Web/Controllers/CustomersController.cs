using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DI.TinyCrm.Core.Entities;
using DI.TinyCrm.Core.Interfaces;
using DI.TinyCrm.Core.Options;

namespace DI.TinyCrm.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var allCustomersResult = await _customerService.GetCustomersAsync();

            return View(allCustomersResult.Data);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);

            if (customer.Error != null || customer.Data == null)
            {
                return NotFound();
            }

            return View(customer.Data);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,VatNumber,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.CreateCustomerAsync(new CreateCustomerOptions
                {
                    Address = customer.Address,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    VatNumber = customer.VatNumber
                });

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            
            if (customer.Error != null || customer.Data == null)
            {
                return NotFound();
            }

            return View(customer.Data);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomerByIdAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allCustomersResult = await _customerService.GetCustomersAsync();

            return Ok(allCustomersResult.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomerByIdAsync(id);

            return NoContent();
        }
    }
}
