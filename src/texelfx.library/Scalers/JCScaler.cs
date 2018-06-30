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
        public byte[] ScaledBytes { get; set; }
    }

    public class JCScaler : BaseScaler
    {

        private const string ModelName = "jcscaler.mdl";

        public override string Name => "JC Scaler";

        public override async Task<ScalerResponseItem> ScaleAsync(int scaleMultiplier, byte[] originalBytes)
        {
            var model = await PredictionModel.ReadAsync<ImageData, ImageDataPrediction>(ModelName);

            var input = new ImageData();

            var prediction = model.Predict(input);

            return new ScalerResponseItem
            {
                ScaledBytes = prediction.ScaledBytes
            };
        }
    }
}