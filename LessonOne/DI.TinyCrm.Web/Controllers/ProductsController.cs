using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DI.TinyCrm.Web.Entities;
using DI.TinyCrm.Web.Interfaces;
using DI.TinyCrm.Web.Options;

namespace DI.TinyCrm.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var allProductsResult = await _productService.GetProductsAsync();

            return View(allProductsResult.Data);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product.Error != null || product.Data == null)
            {
                return NotFound();
            }

            return View(product.Data);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,Price,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(new CreateProductOptions
                {
                    Code = product.Code,
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                });

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product.Error != null || product.Data == null)
            {
                return NotFound();
            }

            return View(product.Data);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductByIdAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
