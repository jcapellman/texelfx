using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.ML;
using Microsoft.ML.Data;

using texelfx.library.Objects;

namespace texelfx.library.Scalers
{
    public class ImageData
    {
        [LoadColumn(0)] public byte[] OriginalBytes;
    }

    public class ImageDataPrediction
    {
        [ColumnName("Score")] public byte[] ScaledBytes;
    }

    public class JCScaler : BaseScaler
    {
        private MLContext _mlContext;

        private const string ModelName = "jcscaler.mdl";

        public override string Name => "JC Scaler";

        public JCScaler()
        {
            _mlContext = new MLContext();
        }

        public override async Task<ScalerResponseItem> ScaleAsync(int scaleMultiplier, byte[] originalBytes)
        {
            try
            {
                ITransformer model;

                using (var fileStream = new FileStream(ModelName, FileMode.Open))
                {
                    model = _mlContext.Model.Load(fileStream);
                }

                var input = new ImageData();

                var prediction = _mlContext.Model.CreatePredictionEngine<ImageData, ImageDataPrediction>(model).Predict(input);

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