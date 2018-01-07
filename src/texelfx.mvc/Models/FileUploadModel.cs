using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace texelfx.mvc.Models
{
    public class FileUploadModel
    {
        public List<string> Scalers { get; set; }

        [Required]
        public IFormFile UploadFile { get; set; }

        [Required]
        public string ScalerType { get; set; }
    }
}