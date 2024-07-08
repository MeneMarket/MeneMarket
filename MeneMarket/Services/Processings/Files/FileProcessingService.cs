using MeneMarket.Services.Foundations.Files;

namespace MeneMarket.Services.Processings.Files
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IFileService fileService;

        public FileProcessingService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async ValueTask<string> UploadFileAsync(
            MemoryStream memoryStream,
            string fileName, int targetSizeKB)
        {
            var uploadsFolder =
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imageFiles");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            return await this.fileService.UploadFileAsync(memoryStream, fileName, uploadsFolder, targetSizeKB);
        }

        public string DeleteImageFile(List<string> filePaths)
        {
            if (filePaths.Count != 0)
            {
                foreach (var filePath in filePaths)
                {

                    if (System.IO.File.Exists("wwwroot/imageFiles/" + Path.GetFileName(filePath)))
                    {
                        this.fileService.RemoveImageFile("wwwroot/imageFiles/" + Path.GetFileName(filePath));
                    }
                    else
                    {
                        return "Image not found.";
                    }
                }

                return "Images deleted successfully.";
            }
            else
            {
                return "file Paths is null";
            }
        }
    }
}