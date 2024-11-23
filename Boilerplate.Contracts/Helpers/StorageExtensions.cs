using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Boilerplate.Contracts.Helpers
{
    public static class StorageExtensions
    {
        private static string StorageDirectoryPath = string.Empty;

        private static string StorageAppURL = string.Empty;

        private static string StorageURLName = string.Empty;

        public static string GetStoragePath(this IConfiguration Configuration)
        {
            StorageDirectoryPath = Configuration.GetValue("Storage:MainPath", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","storage"));
            if (!Directory.Exists(StorageDirectoryPath))
            {
                Directory.CreateDirectory(StorageDirectoryPath);
            }
            Configuration.GetStorageURLName();
            Configuration.GetStorageAppURL();
            return StorageDirectoryPath;
        }

        public static string GetStorageURLName(this IConfiguration Configuration)
        {
            StorageURLName = Configuration.GetValue("Storage:URLPrefix", "/storage");
            return StorageURLName;
        }

        public static string GetStorageAppURL(this IConfiguration Configuration)
        {
            StorageAppURL = Configuration.GetValue("Storage:AppURL", "");
            StorageAppURL += StorageURLName;
            return StorageAppURL;
        }

        public static void UseAppStorage(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var Configuration = serviceProvider.GetRequiredService<IConfiguration>();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Configuration.GetStoragePath()),
                RequestPath = new PathString(StorageURLName)
            });
        }

        public static string GetUrlFromAbsolutePath(this string absolutePath)
        {
            return absolutePath.Replace(StorageDirectoryPath, StorageAppURL).Replace(@"\", "/");
        }

        public static string ToHostUrl(this string absolutePath)
        {
            return ( "/" + absolutePath).Replace(@"\", "/");
        }
    }
}
