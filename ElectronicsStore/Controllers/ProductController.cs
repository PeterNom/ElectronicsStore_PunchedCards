using ElectronicsStore.Models;
using ElectronicsStore.Models.ViewModels;
using ElectronicsStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace ElectronicsStore.Controllers
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            DetailsViewModel detailsViewModel = new DetailsViewModel(product);

            return View(detailsViewModel);
        }
        
        public async Task<IActionResult> CategoryList(int id)
        {
            var productsByCategory = await _productRepository.GetProductsByCategoryAsync(id);

            return View(productsByCategory);
        }
        
        public IActionResult Search()
        {
            return View();
        }
    }
}
