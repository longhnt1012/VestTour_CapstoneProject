using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace VestTour.Repository.FileStorage
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
        {
            // Ensure the uploads directory exists
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), FileConstant.BaseUploadFolder, subfolder);
            Directory.CreateDirectory(uploadsFolder);

            // Generate a unique filename
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Save the file to the specified location
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative URL to the file
            return $"/{FileConstant.BaseUploadFolder}/{subfolder}/{fileName}";
        }
    }
}
