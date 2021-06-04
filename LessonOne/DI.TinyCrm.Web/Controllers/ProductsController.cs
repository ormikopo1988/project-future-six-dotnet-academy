using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DI.TinyCrm.Core.Interfaces;
using DI.TinyCrm.Core.Options;
using DI.TinyCrm.Core.Dtos;

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
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,Price,Quantity")] ProductDto product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(CreateProductOptions.MapFromProductDto(product));

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allProductsResult = await _productService.GetProductsAsync();

            return Ok(allProductsResult.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductByIdAsync(id);

            return NoContent();
        }
    }
}
