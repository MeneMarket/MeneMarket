using MeneMarket.Services.Processings.Files;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : RESTFulController
    {
        private readonly IFileProcessingService fileProcessingService;

        public ImageController(IFileProcessingService fileProcessingService)
        {
            this.fileProcessingService = fileProcessingService;
        }

        [HttpPost]
        public async Task<IActionResult> PostImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not provided or is empty.");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                await fileProcessingService.UploadFileAsync(stream, "Image.WebP", 5);
            }

            return Ok("File uploaded successfully.");
        }

    }
}
