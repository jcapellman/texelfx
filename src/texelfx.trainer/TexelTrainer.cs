using Microsoft.ML.Data;
using Microsoft.ML.Legacy;
using Microsoft.ML.Legacy.Data;
using Microsoft.ML.Legacy.Trainers;
using Microsoft.ML.Legacy.Transforms;

using texelfx.library.Scalers;

namespace texelfx.trainer
{
    public class TexelTrainer
    {
        public async void Train(string modelFileName, string trainingFileName)
        {
            var pipeline = new LearningPipeline
            {
                new TextLoader(trainingFileName).CreateFrom<ImageData>(separator: ','),
                new Dictionarizer("Label"),
                new ColumnConcatenator("Features", "OriginalBytes"),
                new FastTreeBinaryClassifier
                {
                    NumLeaves = 5,
                    NumTrees = 5,
                    MinDocumentsInLeafs = 2
                }
            };
            
            var model = pipeline.Train<ImageData, ImageDataPrediction>();

            await model.WriteAsync(modelFileName);
        }
    }
}