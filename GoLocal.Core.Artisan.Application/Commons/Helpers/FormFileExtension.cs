using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace GoLocal.Core.Artisan.Application.Commons.Helpers
{
    public static class FormFileExtension
    {
        public static async Task<byte[]> ResizeAsync(this IFormFile file, int width = 512, int lenght = 512)
        {
            if (!file.IsImage())
                return null;
            
            var image = await Image.LoadAsync(file.OpenReadStream());

            var before = image.Size();
            
            image.Mutate(x => x.Resize(new ResizeOptions 
            {
                Size = new Size(width, lenght),
                Sampler = KnownResamplers.Lanczos3,
                Compand = true,
                Mode =  ResizeMode.Crop
            }));

            var after = image.Size();
            
            var stream = new MemoryStream();
            
            await image.SaveAsync(stream, new JpegEncoder()
            {
                Quality = 80,
            });

            return stream.GetBuffer();
        }

        private static bool IsImage(this IFormFile file)
            => (file.ContentType.ToLower() == "image/jpg"  ||
                file.ContentType.ToLower() == "image/jpeg" ||
                file.ContentType.ToLower() == "image/png");
    }
}