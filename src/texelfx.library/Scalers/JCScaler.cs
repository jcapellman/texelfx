using System.Numerics;
using System.Threading.Tasks;

using Microsoft.ML;

using texelfx.library.Objects;

namespace texelfx.library.Scalers
{
    public class ImageData
    {

    }

    public class ImageDataPrediction
    {

    }

    public class JCScaler : BaseScaler
    {
        private static readonly Vector2 MODEL_IMAGE_SIZE = new Vector2(1920, 1080);

        public override string Name => "JC Scaler";

        private enum GraphInput
        {
            IMAGE_BYTES = 0
        }

        private enum GraphOutput
        {
            COMPRESSION_ARTIFACTS_DETECTED = 0
        }
        
        public override async Task<ScalerResponseItem> ScaleAsync(int scaleMultiplier, byte[] originalBytes)
        {
            var model = await PredictionModel.ReadAsync<ImageData, ImageDataPrediction>("jcscaler.mdl");


            return new ScalerResponseItem
            {
                ScaledBytes = originalBytes
            };
        }
    }
}