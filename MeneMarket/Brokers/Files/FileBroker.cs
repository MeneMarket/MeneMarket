using System.Drawing;
using System.Drawing.Imaging;

namespace MeneMarket.Brokers.Files
{
    public class FileBroker : IFileBroker
    {
        public async ValueTask<string> SaveFileAsync(
            MemoryStream memoryStream, string fileName, string uploadsFolder, int targetSizeKB)
            {
                // Convert target size from KB to bytes
                int targetSizeBytes = targetSizeKB * 1024;

                string filePath = Path.Combine(uploadsFolder, fileName);
                var relativePath = Path.Combine("imageFiles", fileName);

                try
                {
                    using (var originalImage = Image.FromStream(memoryStream))
                    {
                        long quality = 90L; // Initial quality setting
                        var jpegCodec = GetEncoderInfo("image/jpeg");
                        if (jpegCodec == null)
                        {
                            throw new Exception("JPEG codec not found.");
                        }

                        using (var tempStream = new MemoryStream())
                        {
                            while (true)
                            {
                                var qualityParam = new EncoderParameter(Encoder.Quality, quality);
                                var encoderParams = new EncoderParameters(1);
                                encoderParams.Param[0] = qualityParam;

                                tempStream.SetLength(0); // Reset the stream
                                originalImage.Save(tempStream, jpegCodec, encoderParams);

                                // Check if the image is within the size limit
                                if (tempStream.Length <= targetSizeBytes || quality <= 10)
                                {
                                    break;
                                }

                                quality -= 10; // Decrease quality
                            }

                            // Save the final compressed image to file
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                tempStream.Position = 0;
                                await tempStream.CopyToAsync(fileStream);
                            }
                        }
                    }

                    return relativePath;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to save the file.", ex);
                }
            }

            private static ImageCodecInfo GetEncoderInfo(string mimeType)
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.MimeType == mimeType)
                    {
                        return codec;
                    }
                }
                return null;
            }



    public async Task DeleteImageFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else
                {
                    throw new FileNotFoundException("File not found.", filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete file '{filePath}'.", ex);
            }
        }
    }
}