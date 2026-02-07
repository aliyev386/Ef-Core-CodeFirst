using Ef_Core_CodeFirst.Models.Entities.Concretes;
using Ef_Core_CodeFirst.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Ef_Core_CodeFirst.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //get all
        public async Task<IActionResult> GetAllCategory() => View(await _categoryRepository.GetAllAsync());

        //add
        [HttpGet]
        public IActionResult AddCategory() => View();

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            category.Products = new List<Product>();
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddAsync(category);
                return RedirectToAction("GetAllCategory");
            }
            return View();

        }
        //delete
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existCategory = await _categoryRepository.GetByIdAsync(id);
            if (existCategory != null)
            {
                await _categoryRepository.DeleteAsync(id);
                return RedirectToAction("GetAllCategory");
            }
            ViewBag.Error = "Category not found";
            return View(id);

        }

        public IActionResult NotFound()
        {
            return View();
        }
        //edit
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                return View();
            }
            return View("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var existCategory = await _categoryRepository.GetByIdAsync(category.Id);
                if (existCategory != null)
                {
                    existCategory.Name = category.Name;

                    await _categoryRepository.UpdateAsync(existCategory);
                    return RedirectToAction("GetAllCategory");
                }
                ViewBag.Error = "Category not found";
                return View(category);
            }
            return View(category);
        }
        //get by id
        [HttpGet]
        public IActionResult GetCategoryById() => View();

        [HttpPost]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return View(await _categoryRepository.GetByIdAsync(id));
        }
    }
}
