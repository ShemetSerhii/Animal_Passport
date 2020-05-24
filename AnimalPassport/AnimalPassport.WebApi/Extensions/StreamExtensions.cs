using System.IO;

namespace AnimalPassport.WebApi.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] GetBytes(this Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);

            return bytes;
        }
    }
}