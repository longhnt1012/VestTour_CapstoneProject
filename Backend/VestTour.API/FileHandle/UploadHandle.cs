namespace VestTour.API.FileHandle
{
    public class UploadHandle
    {
        public string Upload(IFormFile file)
        {
            List<string> validExtensions = new List<string>() {".jpg",".png",".gif"};
            string extension =Path.GetExtension(file.FileName);
            if (!validExtensions.Contains(extension)) {
                return $"Extension is not valid({string.Join(',', validExtensions)})";
            }

            long size  = file.Length;
            if (size > (5*1024*1024)) {
                return "Maximum size can be 5mb";
            }

            string fileName = Guid.NewGuid().ToString() +extension;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            using FileStream stream = new FileStream(Path.Combine(path,fileName), FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }
    }
}
