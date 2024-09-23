using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using PhotoMagic.Models;
using System.Text;

namespace PhotoMagic.ChangePhoto
{
    public class PhotoRoom: IChangePhoto
    {

        private IWebHostEnvironment _environment;

        public PhotoRoom(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public List<ImageModel> ChangePhoto(ImageModel image)
        {
            Connection(image);
            return new List<ImageModel>();
        }

        private void Connection(ImageModel image)
        {
            using (var client = new HttpClient())
            {
                var img = File.ReadAllBytes(Path.Combine(_environment.WebRootPath, image.FilePath));

                var webRequest = new HttpRequestMessage(HttpMethod.Post, "https://sdk.photoroom.com/v1/segment")
                {
                    Content = new StringContent($"{{ 'image_file': {img}, 'format': png }}", Encoding.UTF8, "image/png, application/json")
                };

                webRequest.Headers.Add("x-api-key", "b4ca8d262c26d19415b2f3a16b3e38f0d3682f85");

                var response = client.Send(webRequest);

                if (response.IsSuccessStatusCode)
                {
                    
                }
            }
        }
    }
}
