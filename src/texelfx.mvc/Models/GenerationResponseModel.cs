namespace texelfx.mvc.Models
{
    public class GenerationResponseModel
    {
        public string ScalerType { get; set; }

        public byte[] OriginalBytes { get; set; }

        public byte[] ResizedBytes { get; set; }
    }
}