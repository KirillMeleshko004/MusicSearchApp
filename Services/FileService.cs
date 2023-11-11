namespace MusicSearchApp.Services
{
    public class FileService
    {
        public async Task<string?> SaveFile(IFormFile file)
        {
            var fileName = GenerateUniqueFileName(file);
            var folderName = "Temp";
            string filePath = "Data";
            string fullPath = Path.Combine(filePath, folderName, fileName);

            using (Stream fileStream = new FileStream(fullPath, FileMode.Create)) {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine(folderName, fileName);
        }

        private string GenerateUniqueFileName(IFormFile file)
        {
            return Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
        }
    }
}