namespace MusicSearchApp.Services
{
    public class FileService
    {
        public enum FileType
        {
            ProfileImage,
            AlbumImage,
            MusicFile
        }

        private readonly Dictionary<FileType, string> _pathToSave = new Dictionary<FileType, string>()
        {
            {FileType.ProfileImage, "Images/Profile"},
            {FileType.AlbumImage, "Images/Album"},
            {FileType.MusicFile, "Music"},
        };

        private const string root = "Data";
        private const string defaultProfileImage = "Images/Profile/default_profile_img.svg";

        public async Task<string?> SaveFile(IFormFile file, FileType type)
        {
            var fileName = GenerateUniqueFileName(file);


            string filePath = _pathToSave[type];
            string fullPath = Path.Combine(root, filePath, fileName);

            using (Stream fileStream = new FileStream(fullPath, FileMode.Create)) {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine(filePath, fileName);
        }

        public bool DeleteFile(string filePath)
        {
            if(filePath == defaultProfileImage) return true;
            try {
                if (File.Exists(Path.Combine(root, filePath))) {
                    File.Delete(Path.Combine(root, filePath));
                    Console.WriteLine("File deleted.");
                } 
                else {
                    Console.WriteLine("File not found");
                    return false;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private static string GenerateUniqueFileName(IFormFile file)
        {
            return Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
        }
    }
}