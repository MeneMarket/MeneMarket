using System.Data;
using MeneMarket.Models.Foundations.ImageMetadatas;
using MeneMarket.Models.Foundations.Products;
using MeneMarket.Models.Foundations.ProductTypes;
using MeneMarket.Models.Orchestrations.CombinedProducts;
using MeneMarket.Models.Orchestrations.ProductWithCount;
using MeneMarket.Services.Foundations.ImageMetadatas;
using MeneMarket.Services.Foundations.ProductTypes;
using MeneMarket.Services.Processings.Files;
using MeneMarket.Services.Processings.Products;

namespace MeneMarket.Services.Orchestrations.Products
{
    public class ProductOrchestrationService : IProductOrchestrationService
    {
        private readonly IProductProcessingService productProcessingService;
        private readonly IProductTypeService ProductTypeService;
        private readonly IFileProcessingService fileProcessingService;
        private readonly IImageMetadataService imageMetadataService;

        public ProductOrchestrationService(
            IProductProcessingService productProcessingService,
            IFileProcessingService fileProcessingService,
            IProductTypeService ProductTypeService,
            IImageMetadataService imageMetadataService)
        {
            this.productProcessingService = productProcessingService;
            this.ProductTypeService = ProductTypeService;
            this.fileProcessingService = fileProcessingService;
            this.imageMetadataService = imageMetadataService;
        }

        public async ValueTask<Product> AddProductAsync(Product product,
            List<string> bytes64String)
        {
            product.ProductId = Guid.NewGuid();
            product.CreatedDate = DateTimeOffset.Now;

            if (product.ProductTypes == null)
                throw new ArgumentNullException("Product Type is null");

            var storedProduct =
               await this.productProcessingService.AddProductAsync(product);

            if (bytes64String != null)
            {
                foreach (var byte64String in bytes64String)
                {
                    string lowFileName = Guid.NewGuid().ToString() + ".WebP";
                    string mediumFileName = Guid.NewGuid().ToString() + ".WebP";
                    string highFileName = Guid.NewGuid().ToString() + ".WebP";
                    byte[] bytes = Convert.FromBase64String(byte64String);

                    using (var memoryStream = new MemoryStream(bytes))
                    {
                        string lowImageFilePath =
                            await this.fileProcessingService.UploadFileAsync(
                                memoryStream, lowFileName, 5);

                        memoryStream.Seek(0, SeekOrigin.Begin);

                        string mediumImageFilePath =
                            await this.fileProcessingService.UploadFileAsync(
                                memoryStream, mediumFileName, 10);

                        memoryStream.Seek(0, SeekOrigin.Begin);

                        string highImageFilePath =
                            await this.fileProcessingService.UploadFileAsync(
                                memoryStream, highFileName, 20);

                        ImageMetadata imageMetadata = new ImageMetadata
                        {
                            Id = Guid.NewGuid(),
                            LowImageFilePath = lowImageFilePath,
                            MediumImageFilePath = mediumImageFilePath,
                            HightImageFilePath = highImageFilePath,
                            ProductId = product.ProductId,
                        };

                        await this.imageMetadataService.AddImageMetadataAsync(imageMetadata);
                    }
                }

                await this.productProcessingService.ModifyProductAsync(storedProduct);
            }

            if (product.ProductTypes != null)
            {
                foreach (var attribute in storedProduct.ProductTypes)
                {
                    var ProductType = new ProductType
                    {
                        Product = storedProduct,
                        Name = attribute.Name,
                        Count = attribute.Count,
                        ProductId = storedProduct.ProductId
                    };

                    await this.ProductTypeService.AddProductTypeAsync(ProductType);
                }

                return await this.productProcessingService.RetrieveProductByIdAsync(storedProduct.ProductId);
            }
            else
                throw new InvalidDataException("Product is invalid");
        }

        

        public CombinedProducts RetrieveFilteredProducts(string userRole)
        {
            var allProducts = this.productProcessingService.RetrieveAllProducts();
            int producTypesCount = 0;

            foreach (var product in allProducts)
            {
                foreach (var attribute in product.ProductTypes)
                {
                    producTypesCount += attribute.Count;
                }

                if (producTypesCount == 0)
                {
                    product.IsArchived = true;
                    this.productProcessingService.ModifyProductAsync(product);
                }
                else
                    producTypesCount = 0;
            }

            if (userRole == "Admin" || userRole == "Owner")
            {
                var orderedProducts = allProducts.OrderByDescending(product => product.NumberSold);
                IQueryable<Product> newProducts;
                IQueryable<Product> topSoldProducts;

                if (orderedProducts.Count() >= 20)
                    topSoldProducts = orderedProducts.Take(20);
                else
                    topSoldProducts = orderedProducts.Take(orderedProducts.Count());

                if (allProducts.Count() >= 20)
                    newProducts = allProducts.Take(20);
                else
                    newProducts = allProducts.Take(allProducts.Count());

                var combinedProducts = new CombinedProducts
                {
                    NewProducts = newProducts,
                    PopularProducts = topSoldProducts,
                };

                return combinedProducts;
            }
            else
            {
                var nonArchivedProducts = allProducts.Where(product => !product.IsArchived);
                var orderedProducts = nonArchivedProducts.OrderByDescending(product => product.NumberSold);
                IQueryable<Product> topSoldProducts;
                IQueryable<Product> newNonArchivedProducts;

                if (orderedProducts.Count() >= 20)
                    topSoldProducts = orderedProducts.Take(20);
                else
                    topSoldProducts = orderedProducts.Take(orderedProducts.Count());

                if (nonArchivedProducts.Count() >= 20)
                    newNonArchivedProducts = nonArchivedProducts.Take(20);
                else
                    newNonArchivedProducts = nonArchivedProducts.Take(nonArchivedProducts.Count());

                var combinedProducts = new CombinedProducts
                {
                    AllProducts = nonArchivedProducts,
                    NewProducts = newNonArchivedProducts,
                    PopularProducts = topSoldProducts,
                };

                return combinedProducts;
            }
        }

        public ProductsWithCount RetrieveAllProducts(string userRole, int pageNumber)
        {
            var allProducts = this.productProcessingService.RetrieveAllProducts();

            if (userRole == "Admin" || userRole == "Owner")
            {
                    int pageSize = 5;
                    int productsToSkip = (pageNumber - 1) * pageSize;
                    var products = this.productProcessingService.RetrieveAllProducts()
                                               .Skip(productsToSkip)
                                               .Take(pageSize);

                    return new ProductsWithCount
                    {
                        allProducts = products,
                        ProductsCount = (ulong)allProducts.Count()
                    };
            }
            else
            {
                    int pageSize = 5;
                    int productsToSkip = (pageNumber - 1) * pageSize;
                    var nonArchivedallProducts = this.productProcessingService.RetrieveAllProducts().Where(p => p.IsArchived == false);
                    var products = this.productProcessingService.RetrieveAllProducts()
                                               .Skip(productsToSkip)
                                               .Take(pageSize);

                    return new ProductsWithCount
                    {
                        allProducts = products,
                        ProductsCount = (ulong)nonArchivedallProducts.Count()
                    };
            }
        }

        public async ValueTask<Product> RetrieveProductByIdAsync(Guid id) =>
            await this.productProcessingService.RetrieveProductByIdAsync(id);

        public async ValueTask<Product> ModifyProductAsync(
            Product product,
           List<string> bytes64String)
        {
            if (product.ProductTypes == null)
                throw new ArgumentNullException("Product Type is null");

            var storedProduct =
               await this.productProcessingService.RetrieveProductByIdAsync(product.ProductId);

            if (bytes64String != null)
            {
                foreach (var byte64String in bytes64String)
                {
                    string lowFileName = Guid.NewGuid().ToString() + ".WebP";
                    string mediumFileName = Guid.NewGuid().ToString() + ".WebP";
                    string highFileName = Guid.NewGuid().ToString() + ".WebP";
                    byte[] bytes = Convert.FromBase64String(byte64String);

                    using (var memoryStream = new MemoryStream(bytes))
                    {
                        string lowImageFilePath =
                            await this.fileProcessingService.UploadFileAsync(
                                memoryStream, lowFileName, 5);

                        memoryStream.Seek(0, SeekOrigin.Begin);

                        string mediumImageFilePath =
                            await this.fileProcessingService.UploadFileAsync(
                                memoryStream, mediumFileName, 10);

                        memoryStream.Seek(0, SeekOrigin.Begin);

                        string highImageFilePath =
                            await this.fileProcessingService.UploadFileAsync(
                                memoryStream, highFileName, 20);

                        ImageMetadata imageMetadata = new ImageMetadata
                        {
                            Id = Guid.NewGuid(),
                            LowImageFilePath = lowImageFilePath,
                            MediumImageFilePath = mediumImageFilePath,
                            HightImageFilePath = highImageFilePath,
                            ProductId = product.ProductId,
                        };

                        await this.imageMetadataService.AddImageMetadataAsync(imageMetadata);
                    }
                }
            }

            var AllImageMetadatas =
                this.imageMetadataService.RetrieveAllImageMetadatas().Where(image => image.ProductId == product.ProductId);

            foreach (var imagedata in AllImageMetadatas)
            {
                var selectedimageMetadata =
                    product.ImageMetadatas.FirstOrDefault(i => i.Id == imagedata.Id);

                if (selectedimageMetadata == null)
                {
                    List<string> imageFilePaths = new List<string>();
                    imageFilePaths.Add(imagedata.LowImageFilePath);
                    imageFilePaths.Add(imagedata.MediumImageFilePath);
                    imageFilePaths.Add(imagedata.HightImageFilePath);
                    this.fileProcessingService.DeleteImageFile(imageFilePaths);
                    await this.imageMetadataService.RemoveImageMetadataByIdAsync(imagedata.Id);
                }
            }

            foreach (var attribute in product.ProductTypes)
            {
                var productType = await this.ProductTypeService.RetrieveProductTypeByIdAsync(attribute.ProductTypeId);
                if (productType != attribute)
                    await this.ProductTypeService.ModifyProductTypeAsync(attribute);
                else if (productType == null)
                    await this.ProductTypeService.AddProductTypeAsync(attribute);
            }

            return await this.productProcessingService.ModifyProductAsync(product);
        }

        public async ValueTask<Product> RemoveProductAsync(Guid id)
        {
            var product =
                await this.productProcessingService.RetrieveProductByIdAsync(id);

            foreach (var imageMetadata in product.ImageMetadatas)
            {
                List<string> imageFilePaths = new List<string>();
                imageFilePaths.Add(imageMetadata.LowImageFilePath);
                imageFilePaths.Add(imageMetadata.MediumImageFilePath);
                imageFilePaths.Add(imageMetadata.HightImageFilePath);
                fileProcessingService.DeleteImageFile(imageFilePaths);
            }

            return await this.productProcessingService.RemoveProductByIdAsync(id);
        }

        public async ValueTask<Product> ArchiveAndUnArchiveProduct(Guid id)
        {
            var product = await this.productProcessingService.RetrieveProductByIdAsync(id);

            if (product == null)
                throw new NullReferenceException("Product is Null");
            else if (product.IsArchived)
                product.IsArchived = false;
            else if (!product.IsArchived)
                product.IsArchived = true;

            return product;
        }
    }
}