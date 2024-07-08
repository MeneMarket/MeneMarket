using MeneMarket.Models.Foundations.Products;
using MeneMarket.Models.Orchestrations.CombinedProducts;
using MeneMarket.Models.Orchestrations.ProductWithCount;
using MeneMarket.Models.Orchestrations.ProductWithImages;
using MeneMarket.Services.Orchestrations.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : RESTFulController
    {
        private readonly IProductOrchestrationService productOrchestrationService;

        public ProductController(
            IProductOrchestrationService productOrchestrationService)
        {
            this.productOrchestrationService = productOrchestrationService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        public async ValueTask<ActionResult<Product>> PostProductAsync(ProductWithImages productWithImages) =>
            await this.productOrchestrationService.AddProductAsync(productWithImages.Product, productWithImages.bytes);

        [HttpGet]
        [Route("All")]
        public ProductsWithCount GetAllProducts(string userRoleString, int pageNumber)
        {
            return productOrchestrationService.RetrieveAllProducts(userRoleString, pageNumber);
        }

        [HttpGet]
        public CombinedProducts GetFilteredProducts(string userRoleString)
        {
            return productOrchestrationService.RetrieveFilteredProducts(userRoleString);
        }

        [HttpGet("ById")]
        public async ValueTask<ActionResult<Product>> GetProductByIdAsync(Guid id) =>
            await this.productOrchestrationService.RetrieveProductByIdAsync(id);

        [HttpPut]
        [Authorize(Roles = "Admin,Owner")]
        public async ValueTask<ActionResult<Product>> PutProductAsync(ProductWithImages productWithImages) =>
            await this.productOrchestrationService.ModifyProductAsync(
                productWithImages.Product,
                productWithImages.bytes);

        [HttpPut]
        [Route("Archive And UnArchive")]
        [Authorize(Roles = "Admin,Owner")]
        public async ValueTask<ActionResult<Product>> ArchiveAndUnArchiveProduct(Guid id) =>
            await this.productOrchestrationService.ArchiveAndUnArchiveProduct(id);

        [HttpDelete]
        [Authorize(Roles = "Admin,Owner")]
        public async ValueTask<ActionResult<Product>> DeleteProductAsync(Guid id) =>
            await this.productOrchestrationService.RemoveProductAsync(id);
    }
}