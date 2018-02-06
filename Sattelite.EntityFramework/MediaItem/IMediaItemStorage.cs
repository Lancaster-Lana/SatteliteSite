namespace Sattelite.EntityFramework.MediaItem
{
    using System.IO;

    public interface IMediaNewsStorage
    {
        string Storage(MemoryStream stream, string fileName);
    }
}