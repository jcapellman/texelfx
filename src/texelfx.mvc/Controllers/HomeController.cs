using System;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using texelfx.library;
using texelfx.library.Common;
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

        private BaseScaler GetScaler(string name) => ScalerManager.GetScalers().FirstOrDefault(a => a.Name == name);

        private byte[] GetBytesFromPost(IFormFile file)
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

            var aspectRatio = ((float)response.ScalledDimensions.width / response.ScalledDimensions.height);

            (int height, int width) renderDimensions = (800, Convert.ToInt32(800 / aspectRatio)); 

            return View("Generation", new GenerationResponseModel
            {
               OriginalBytes = fileBytes,
               ScalerType = model.ScalerType,
               ResizedBytes = response.ScaledBytes,
               Dimensions = renderDimensions
            });
        }
    }
}