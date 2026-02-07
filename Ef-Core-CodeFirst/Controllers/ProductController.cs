using Ef_Core_CodeFirst.Models.Entities.Concretes;
using Ef_Core_CodeFirst.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Ef_Core_CodeFirst.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult NotFound()
        {
            return View();
        }
        //get all
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllWithCategoryAsync();
            return View(products);

        }
        //add
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {

            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return RedirectToAction("GetAllProducts");
            }
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }
        //delete
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existProduct = await _productRepository.GetByIdAsync(id);
            if (existProduct != null)
            {
                await _productRepository.DeleteAsync(id);
                return RedirectToAction("GetAllProducts");
            }
            ViewBag.Error = "Product not found";
            return View(id);

        }

        //edit
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                ViewBag.Categories = await _categoryRepository.GetAllAsync();
                return View(product);
            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var existProduct = await _productRepository.GetByIdAsync(product.Id);
                existProduct.Name = product.Name;
                existProduct.Price = product.Price;
                existProduct.CategoryId = product.CategoryId;
                
                await _productRepository.UpdateAsync(existProduct);
                return RedirectToAction("GetAllProducts");
            }
            return View(product);
        }
        //get by id
        [HttpGet]
        public IActionResult GetProductById() => View();

        [HttpPost]
        public async Task<IActionResult> GetProductById(int id)
        {
            return View(await _productRepository.GetProductByIdWithCategoryAsync(id));
        }

    }
}
