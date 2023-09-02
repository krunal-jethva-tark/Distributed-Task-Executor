using Microsoft.AspNetCore.Http;

namespace DTS.Models.Utility
{
    public class FileUtility
    {
        public static string UploadFile(string fileLocation, IFormFile file)
        {
            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }

            string filePath = Path.Combine(fileLocation, file.Name);
            using var stream = File.Create(filePath);
            file.CopyTo(stream);

            var ext = new FileInfo(file.FileName).Extension;
            var fileNameWithExt = Path.GetFileNameWithoutExtension(file.FileName) + ext;

            return fileNameWithExt;
        }
    }
}
