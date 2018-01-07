namespace texelfx.mvc.Models
{
    public class GenerationResponseModel
    {
        public string ScalerType { get; set; }

        public byte[] OriginalBytes { get; set; }

        public byte[] ResizedBytes { get; set; }

        public (int width, int height) Dimensions { get; set; }

        public (int width, int height) ScaledDimensions { get; set; }
    }
}