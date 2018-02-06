namespace Sattelite.Framework.Extensions
{
    using System.IO;

    public static class MemoryStreamExtension
    {
         public static void WriteTo(this MemoryStream memoryStream, string fileName)
         {
            using (var outStream = File.OpenWrite(fileName))
            {
                memoryStream.WriteTo(outStream);
                outStream.Flush();
            }
         }
    }
}