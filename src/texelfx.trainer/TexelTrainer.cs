using System.IO;
using Microsoft.ML;

using texelfx.library.Scalers;

namespace texelfx.trainer
{
    public class TexelTrainer
    {
        private MLContext _mlContext;

        public TexelTrainer()
        {
            _mlContext = new MLContext();
        }

        public void Train(string modelFileName, string trainingFileName)
        {
            var dataView = _mlContext.Data.LoadFromTextFile<ImageData>(trainingFileName, hasHeader: false);

            var model = _mlContext.BinaryClassification.Trainers.FastTree();

            var fitModel = model.Fit(dataView);

            using (var fs = new FileStream(modelFileName, FileMode.Create))
            {
                _mlContext.Model.Save(fitModel, fs);
            }
        }
    }
}