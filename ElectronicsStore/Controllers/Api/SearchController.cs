using ElectronicsStore.Models;
using ElectronicsStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;

namespace ElectronicsStore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public SearchController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productRepository.GetProductByIdAsync(id);

            if(result==null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> searchProduct([FromBody] string searchQuery)
        {
            IEnumerable<Product> products = new List<Product>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = await _productRepository.SearchProductAsync(searchQuery);
            }

            return new JsonResult(products);
        }
    }
}
