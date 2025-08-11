using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Utilities
{
    public static class FileHelper
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".png" };
        private static readonly long MaxFileSize = 5 * 1024 * 1024;

        private static bool IsValidFile(IFormFile? file, out string message)
        {
            message = string.Empty;

            if (file == null)
            {
                message = "no file uploaded";
                return false;
            }

            if (file.Length > MaxFileSize)
            {
                message = $"file size exceeded max size of file, 5 mg.";
                return false;
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(fileExtension))
            {
                message = "file Extension is not supported.";
                return false;
            }

            return true;
        }

        public static string UploadFile(IFormFile? file, string rootPath, string? filePath)
        {
            if (file == null || filePath == null)
                return string.Empty;

            if (!IsValidFile(file, out string message))
                throw new InvalidOperationException(message);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName).ToLower();

            
            filePath = filePath.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            var folderPath = Path.Combine(rootPath, filePath);
            Directory.CreateDirectory(folderPath);

            var destinationPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(destinationPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine(filePath, fileName).Replace("\\", "/"); 
        }



        public static string UpdateFile(IFormFile? file, string rootPath, string? filePath, string oldFileName = "")
        {
            if (filePath == null)
                return string.Empty;

            if (file != null)
            {
                if (!string.IsNullOrEmpty(oldFileName))
                {
                    RemoveFile(rootPath, filePath, Path.GetFileName(oldFileName));
                }

                return UploadFile(file, rootPath, filePath);
            }

            filePath = filePath.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return oldFileName;
        }

        public static bool RemoveFile(string rootPath, string? filePath, string fileName)
        {
            if (filePath == null)
                return false;

            var oldFilePath = Path.Combine(rootPath, filePath, fileName);
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }

            return true;
        }
    }
}
