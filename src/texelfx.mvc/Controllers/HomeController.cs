using System;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using texelfx.library;
using texelfx.library.Managers;

using texelfx.mvc.Models;

namespace texelfx.mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View(new FileUploadModel
        {
            Scalers = ScalerManager.GetScalers().Select(a => a.Name).ToList()
        });

        private static BaseScaler GetScaler(string name) => ScalerManager.GetScalers().FirstOrDefault(a => a.Name == name);

        private static byte[] GetBytesFromPost(IFormFile file)
        {
            using (var ms = new BinaryReader(file.OpenReadStream()))
            {
                return ms.ReadBytes((int)file.Length);
            }
        }

        [HttpPost]
        public IActionResult Generate(FileUploadModel model)
        {
            var generator = GetScaler(model.ScalerType);

            if (generator == null)
            {
                throw new Exception($"{model.ScalerType} was not found");
            }

            var fileBytes = GetBytesFromPost(model.UploadFile);

            var response = generator.Scale(2, fileBytes);

            if (response.HasError)
            {
                return View("Error");
            }

            var aspectRatio = ((float)response.ScaledDimensions.width / response.ScaledDimensions.height);

            (int height, int width) renderDimensions = (800, Convert.ToInt32(800 / aspectRatio)); 

            return View("Generation", new GenerationResponseModel
            {
               OriginalBytes = fileBytes,
               ScalerType = model.ScalerType,
               ResizedBytes = response.ScaledBytes,
               Dimensions = renderDimensions,
               ScaledDimensions = response.ScaledDimensions
            });
        }
    }
}