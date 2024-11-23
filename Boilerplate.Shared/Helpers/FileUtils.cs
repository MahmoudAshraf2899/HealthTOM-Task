using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Boilerplate.Shared.Helpers
{
    public class FileUtils
    {
        public static byte[] ToBytes(IFormFile file)
        {
            byte[] data = null;
            if (file is not null && file.Length > 0)
            {
                var stream = file.OpenReadStream();
                using (var br = new BinaryReader(stream))
                    data = br.ReadBytes((int) stream.Length);
            }
            if (data is not null && data.Length > 0)
                return data;
            else
                return null;
        }

        public static byte[] ToBytes(string base64String)
        {
            if(!string.IsNullOrEmpty(base64String))
                return Convert.FromBase64String(base64String);
            return null;
        }

        public static IFormFile ToIFormFile(byte[] byteArray, string fileName)
        {
            if (byteArray is not null && byteArray.Length > 0)
            {
                var stream = new MemoryStream(byteArray);
                return new FormFile(stream, 0, byteArray.Length, "file", fileName);
            }
            return null;
        }
        public static string ToBase64String(byte[] byteArray)
        {
            if (byteArray is not null && byteArray.Length > 0)
            {
                return Convert.ToBase64String(byteArray, 0, byteArray.Length);
            }
            return null;
        }

        public static string ToBase64String(IFormFile file)
        {
            return ToBase64String(ToBytes(file));
        }

        public static IFormFile ToIFormFile(string base64String, string fileName)
        {
            return ToIFormFile(ToBytes(base64String), fileName);
        }
    }
}
