namespace Sattelite.EntityFramework.Extensions
{
    using System.IO;
    using System.Web;

    using Sattelite.EntityFramework.MediaItem;

    public static class HttpPostedFileExtension
    {
        public static string CreateImagePathFromStream(this HttpPostedFileBase postedFile, IMediaNewsStorage imageStorage)
        {
            var imagePath = string.Empty;

            if (postedFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    postedFile.InputStream.CopyTo(memoryStream);

                    imagePath = imageStorage.Storage(memoryStream, postedFile.FileName);
                }
            }

            return imagePath;
        }
    }
}