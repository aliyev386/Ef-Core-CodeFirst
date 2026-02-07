using Ef_Core_CodeFirst.Models.Entities.Concretes;
using Ef_Core_CodeFirst.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Ef_Core_CodeFirst.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult NotFound()
        {
            return View();
        }
        //get all
        public async Task<IActionResult> GetAllOrders() => View(await _orderRepository.GetAllOrdersWithProduct());
        //get by id
        [HttpGet]
        public IActionResult GetOrderById() => View();

        [HttpPost]
        public async Task<IActionResult> GetOrderById(int id) => View(await _orderRepository.GetOrderByIdWithProducts(id));
        //add
        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            ViewBag.Products = await _productRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.AddAsync(order);
                return RedirectToAction("GetAllOrders");
            }
            ViewBag.Products = await _productRepository.GetAllAsync();
            return View(order);
        }
        //delete
        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var existOrder = await _orderRepository.GetByIdAsync(id);
            if (existOrder != null)
            {
                await _orderRepository.DeleteAsync(id);
                return RedirectToAction("GetAllOrders");
            }
            return RedirectToAction("NotFound");
        }
        //edit
        [HttpGet]
        public async Task<IActionResult> EditOrder(int id)
        {
            var existOrder = await _orderRepository.GetByIdAsync(id);
            if (existOrder != null)
            {
                ViewBag.Products = await _productRepository.GetAllAsync();
                return View(existOrder);
            }
            return RedirectToAction("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(Order order, int[] selectedProductIds)
        {
            if (ModelState.IsValid)
            {
                var existOrder = await _orderRepository.GetByIdAsync(order.Id);
                if (existOrder != null)
                {
                    existOrder.Products = new List<Product>();
                    foreach (var id in selectedProductIds)
                    {
                        var product = await _productRepository.GetByIdAsync(id);
                        if (product != null)
                        {
                            existOrder.Products.Add(product);
                        }
                    }

                    existOrder.OrderDate = order.OrderDate;

                    await _orderRepository.UpdateAsync(existOrder);
                    return RedirectToAction("GetAllOrders");
                }
                return RedirectToAction("NotFound");
            }
            return View(order);
        }

    }
}
