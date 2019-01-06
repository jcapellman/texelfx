using System;
using System.Threading.Tasks;

using Microsoft.ML.Legacy;
using Microsoft.ML.Runtime.Api;

using texelfx.library.Objects;

namespace texelfx.library.Scalers
{
    public class ImageData
    {
        [Column(ordinal: "0")] public byte[] OriginalBytes;
    }

    public class ImageDataPrediction
    {
        [ColumnName("Score")] public byte[] ScaledBytes;
    }

    public class JCScaler : BaseScaler
    {
        private const string ModelName = "jcscaler.mdl";

        public override string Name => "JC Scaler";

        public override async Task<ScalerResponseItem> ScaleAsync(int scaleMultiplier, byte[] originalBytes)
        {
            try
            {
                var model = await PredictionModel.ReadAsync<ImageData, ImageDataPrediction>(ModelName);

                var input = new ImageData();

                var prediction = model.Predict(input);

                return new ScalerResponseItem
                {
                    ScaledBytes = prediction.ScaledBytes
                };
            }
            catch (Exception ex)
            {
                return new ScalerResponseItem(ex);
            }
        }
    }
}